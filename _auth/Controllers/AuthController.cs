
using auth.api.Data;
using auth.server.jwt.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace auth.api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;



        private readonly AppSettings _appSettings;
        private readonly IMapper mapper;
        private string _errorMessage = string.Empty;

        public AuthController(

            SignInManager<ApplicationUser> siManager,
            UserManager<ApplicationUser> uManager,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            signInManager = siManager;
            userManager = uManager;
            _appSettings = appSettings.Value;
            this.mapper = mapper;


        }



        #region Fetch

        [HttpGet("current-user/claims")]
        public async Task<IActionResult> GetClaims()
        {
            var a = userManager.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var list = new List<UserDto>();


            var claims = await userManager.GetClaimsAsync(a);
            list.Add(new UserDto
            {
                AzureId = a.AzureId,
                Email = a.Email,
                Id = a.Id,
                Claims = claims.Select(x => new UserClaim { Type = x.Type, Value = x.Value }).ToArray(),
            });

            return Ok(list);

        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpGet("account/list")]
        public async Task<ActionResult> GetIdentityUsers()
        {
            var users = userManager.Users.ToList();


            var list = new List<UserDto>();


            foreach (var a in users)
            {
                var u = new UserDto
                {
                    AzureId = a.AzureId,
                    Email = a.Email,
                    Id = a.Id,
                    Claims = (await userManager.GetClaimsAsync(a)).Select(x => new UserClaim { Type = x.Type, Value = x.Value }).ToArray()
                };

                list.Add(u);
            }

               
        

            return Ok(list);
        }

        [Authorize(Roles = "superadmin,admin")]
        [HttpGet("claims/{email}/flush")]
        public async Task<IActionResult> ClearClaims(string email)
        {
            var user = userManager.Users.FirstOrDefault(x => x.Email == email);
            await userManager.RemoveClaimsAsync(user, await userManager.GetClaimsAsync(user));

            return Ok(await userManager.GetClaimsAsync(user));
        }

        #endregion

        /// <summary>
        /// Logs in user using either email + office 365 id 
        /// OR username + password
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AzureAuthenticate([FromBody] LoginModel login)
        {
            string errorMsg = string.Empty;

            string token = string.Empty;

            List<Claim> claims = new List<Claim>();

            try
            {
                var user = await userManager.FindByEmailAsync(login.Email);

                if (user == null) throw new Exception($"User with email: {login.Email} not found.");
                Debug.Print($"User is {user.Email} {user.AzureId}");

                var canLogin = await signInManager.CheckPasswordSignInAsync(user, login.Key, false);

                //if able to login - meaning used user + password successfully
                if (canLogin.Succeeded)
                {
                    //create claims based on roles / and application permission
                    token = CreateToken(user, out claims);
                }
                else
                {
                    Guid azureId = Guid.Empty;

                    //might have used office 365 - so we check if the key is a Guid
                    if (!Guid.TryParse(login.Key, out azureId)) throw new Exception("Invalid login credential");

                    if (!user.AzureId.HasValue)
                    {
                        if (user.AzureId.Value == Guid.Empty)
                        {
                            user.AzureId = azureId;

                            await userManager.UpdateAsync(user);

                            //we get updated data of the user
                            user = await userManager.FindByEmailAsync(login.Email);


                        }
                        else
                        {
                            if (user.AzureId != azureId) throw new Exception("Invalid login credential.");
                        }
                    }

                    token = CreateToken(user, out claims);


                }

                return Ok(new { Token = token, User = user, Claims = claims.ToDictionary(a => a.Type, a => a.Value.ToString()) });
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            return BadRequest($"User not authenticated." + @"\n" + errorMsg);
        }


        #region Create
        [AllowAnonymous]
        [HttpPost("create")]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<object>> Create([FromBody] CreateUserModel model)
        {
            try
            {
                var id = new ApplicationUser
                {
                    Email = model.Email.ToLower(),
                    UserName = model.Email.ToLower(),
                    EmailConfirmed = true,
                };

                if(model.AzureId.HasValue){
                    id.AzureId= model.AzureId;
                }

                var result = await userManager.CreateAsync(id, string.IsNullOrWhiteSpace(model.Password) ? _appSettings.DefaultKey : model.Password);

                if(!result.Succeeded) throw new Exception("Cannot create account",new Exception(string.Join(",",result.Errors.Select(x=>x.Description))));

                var token = await userManager.GenerateEmailConfirmationTokenAsync(id);

                //assign default role
                await userManager.AddToRoleAsync(id, "guest");

                return Created(this.Request.Path, new { Result = token });

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                if (ex.InnerException != null) _errorMessage += "\n" + ex.InnerException.Message;
            }

            return BadRequest(_errorMessage);
        }

        [HttpPost("create-admin")]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<object>> CreateAdmin([FromBody] CreateUserModel model)
        {
            try
            {

                var id = new ApplicationUser
                {
                    AzureId = model.AzureId,
                    Email = model.Email.ToLower(),
                    UserName = model.Email.ToLower(),
                    EmailConfirmed = true,
                };
                var T = await userManager.CreateAsync(id, string.IsNullOrWhiteSpace(model.Password) ? _appSettings.DefaultKey : model.Password);


                //assign default role
                await userManager.AddToRoleAsync(id, "superadmin");

                return Created(this.Request.Path, new { Result = new { Id = id.Id, Email = id.Email }, Success = true, Message = "Created user - success" });


            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.InnerException?.Message, });
            }
        }

        [AllowAnonymous]
        [HttpGet("email/{email}/confirm")]
        public async Task<ActionResult> ConfirmEmail(string email, string token)
        {
            string err = string.Empty;
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                var result = await userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return Ok("Email confirmed.");
                }
                else
                {
                    throw new Exception(string.Join(",", result.Errors));
                }

            }
            catch (Exception ex)
            {
                err = ex.Message;

                if (ex.InnerException != null)
                    err += ex.InnerException.Message;
            }

            return BadRequest(new { Success = false, Message = err });
        }

        #endregion

        #region Update

        [Authorize(Roles = "superadmin,admin")]
        [HttpPost("user/{id}/claims/update")]
        public async Task<IActionResult> AddOrUpdatePermission(string id, [FromBody] string[] permissions)
        {
            if (permissions.Any(a => string.IsNullOrWhiteSpace(a))) ModelState.AddModelError("AsClaim", "Action and Controller cannot be empty");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await userManager.FindByIdAsync(id);

                var claims = await userManager.GetClaimsAsync(user);

                await userManager.RemoveClaimsAsync(user, claims.Where(a => a.Type == "Permission"));

                await userManager.AddClaimsAsync(user,
                    permissions.Select(a => new Claim("Permission", a)));

                var newClaims = await userManager.GetClaimsAsync(user);

                return Ok(new { user, newClaims });

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                if (ex.InnerException != null) _errorMessage += ex.InnerException.Message;
            }
            return BadRequest(_errorMessage);
        }


        [AllowAnonymous]
        [HttpGet("password/{email}/reset/token")]
        public async Task<ActionResult<object>> RequestPasswordResetToken(string email)
        {

            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null) throw new Exception("Invalid email.");

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                return Created(Request.Path, token);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return BadRequest(_errorMessage);

        }

        [AllowAnonymous]
        [HttpPost("password/change")]
        public async Task<ActionResult<object>> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword) throw new Exception("Password does not match.");

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null) throw new Exception("Invalid email or username.");

                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                return Ok(new { Success = result.Succeeded });
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return BadRequest(_errorMessage);
        }

        #endregion

        #region Private
        private List<Claim> RoleBaseClaims(ApplicationUser user, string[] roles)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                };

            if (roles.Any())
                claims.AddRange(roles.Select(a => new Claim(ClaimTypes.Role, a)));


            return claims;
        }

        private string CreateToken(ApplicationUser user, out List<Claim> claims)
        {
            var roles = userManager.GetRolesAsync(user).Result;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            //create claims based on roles / and application permission
            claims = new List<Claim>();

            //add role-based claims
            claims.AddRange(RoleBaseClaims(user, roles.ToArray()).AsEnumerable());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10D),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}