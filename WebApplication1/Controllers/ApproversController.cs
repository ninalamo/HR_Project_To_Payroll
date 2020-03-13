using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain;
using persistence;
using Microsoft.AspNetCore.Identity;
using HR.Application.cqrs.Approver.Queries;
using HR.Application.cqrs.Employee.Queries;
using WebApplication1.Models.Approvers;
using HR.Application.cqrs.Approver.Commands;
using WebApplication1.Extensions;
using System.Security.Claims;
using lib.common;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class ApproversController : BaseController
    {
        public ApproversController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }


        // GET: Approvers
        public async Task<IActionResult> Index()
        {

            return View(await Mediator.Send(new GetApprovers_Request()));
        }

        // GET: Approvers/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var approver = await _context.Approvers
        //        .Include(a => a.Employee)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (approver == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(approver);
        //}

        // GET: Approvers/Create
        public IActionResult Create()
        {
            ViewData["Employee"] = GetEmployeeListsForDropDown().Result;
            return View();
        }

        // POST: Approvers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyEmail,Level,TypeOfRequest")] CreateApproverViewModel model)
        {
            SelectList list = await GetEmployeeListsForDropDown();

            if (ModelState.IsValid)
            {
                try
                {
                    //if 
                    var account = await UserManager.FindByEmailAsync(model.CompanyEmail);

                    if (account == null) throw new Exception($"User has no account yet. Have the user register first so his/her profile gets activated.");

                    var claims = (await UserManager.GetClaimsAsync(account)).ToList();


                    if (claims.Any(i => i.Type == model.TypeOfRequest.ToString() && i.Value == "CanApprove"))
                    {
                        //remove
                        var removeMe = claims.FirstOrDefault(i => i.Type == model.TypeOfRequest.ToString() && i.Value == "CanApprove");
                        await UserManager.RemoveClaimAsync(account, removeMe);
                    }

                    var result = await UserManager.AddClaimAsync(account, new Claim(model.TypeOfRequest.ToString(), "CanApprove"));

                    if (result.Succeeded)
                    {
                        await Mediator.Send(new CreateApprover_Request
                        {
                            CompanyEmail = model.CompanyEmail,
                            Level = model.Level,
                            TypeOfRequest = model.TypeOfRequest,
                            CreatedBy = User.Identity.Name,
                            ModifiedBy = User.Identity.Name
                        });

                        return RedirectToAction(nameof(Index)).WithSuccess("Success", "Added approver");
                    }

                    throw new Exception(string.Join("|", result.Errors.Select(i => i.Description).ToArray()));

                }
                catch (Exception ex)
                {
                    ViewData["Employee"] = list;
                    return base.View(model).WithDanger("Error", ex.GetExceptionMessage());
                }
            }

            ViewData["Employee"] = list;

            return View(model);
        }

       



        // GET: Approvers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approver = await Mediator.Send(new GetApproverByID_Request { ApproverID = id.Value });
            if (approver == null || approver.Result == null)
            {
                return NotFound();
            }

            ViewData["Name"] = approver.Result.FullName;

            return View(new UpdateApproverViewModel { 
                Level =  approver.Result.Level,
                TypeOfRequest = approver.Result.TypeOfRequest,
                IsActive = approver.Result.IsActive,
                ApproverID = approver.Result.ApproverID
            });
        }

        // POST: Approvers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ApproverID,Level,TypeOfRequest,IsActive")] UpdateApproverViewModel model)
        {
            if (id != model.ApproverID)
            {
                return NotFound();
            }

            var approver = (await Mediator.Send(new GetApproverByID_Request { ApproverID = model.ApproverID })).Result;
            ViewData["Name"] = approver.FullName;

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await UserManager.FindByEmailAsync(approver.CompanyEmail);
                    var claims = await UserManager.GetClaimsAsync(user);

                    if (claims.Any(i => i.Type == model.TypeOfRequest.ToString() && i.Value == "CanApprove"))
                    {
                        var removeMe = claims.FirstOrDefault(i => i.Type == model.TypeOfRequest.ToString() && i.Value == "CanApprove");
                        //do not do anything
                        await UserManager.RemoveClaimAsync(user, removeMe);
                    }

                    var result = await UserManager.AddClaimAsync(user, new Claim(model.TypeOfRequest.ToString(), "CanApprove"));

                    if (result.Succeeded)
                    {
                        await Mediator.Send(new UpdateApprover_Request
                        {
                            ApproverID = model.ApproverID,
                            IsActive = model.IsActive,
                            Level = model.Level,
                            TypeOfRequest = model.TypeOfRequest
                        });


                        return RedirectToAction(nameof(Index)).WithSuccess("Success", "Updated approver settings.");
                    }

                    throw new Exception(string.Join("|", result.Errors.Select(i => i.Description).ToArray()));

                }
                catch (Exception ex)
                {
                    return View(model).WithDanger("Error", ex.GetExceptionMessage());
                }
            }

            return RedirectToAction(nameof(Index)).WithWarning("Failed", "Action was cancelled. Should this happen again please contact your developer.");
        }

        // GET: Approvers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approver = await Mediator.Send(new GetApproverByID_Request { ApproverID = id.Value });

            if (approver.Result == null)
            {
                return NotFound();
            }

            return View(approver);
        }

        // POST: Approvers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var approver = await Mediator.Send(new GetApproverByID_Request { ApproverID = id });

            return View(approver).WithWarning("Action cancelled", "Not yet implemented.");
            //return RedirectToAction(nameof(Index));
        }


        private async Task<SelectList> GetEmployeeListsForDropDown()
        {
            var accounts = await UserManager.Users.Select(i => i.Email.ToLower()).ToArrayAsync();
            var response = await Mediator.Send(new GetEmployees_Request { PageNumber = 1, PageSize = 1000 });
            var list = new SelectList(response.Data.Where(i => i.CanApprove && accounts.Contains(i.CompanyEmail.ToLower())), "CompanyEmail", "FullName");
            return list;
        }


    }
}
