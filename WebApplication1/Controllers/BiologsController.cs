using HR.Application.cqrs.Employee.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BiologsController : BaseController
    {
        public BiologsController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody]BiologModel model)
        {
            if (string.IsNullOrEmpty(model.EmployeeNumber))
            {
                ModelState.AddModelError("EmployeeNumber", "Required");
            }

            if (!ModelState.IsValid) return View(ModelState);
            try
            {
                await Mediator.Send(new CreateBioLogRequest
                {
                    EmployeeNumber = model.EmployeeNumber,
                    Lat = double.Parse(model.Lat),
                    Long = double.Parse(model.Long),
                    LogType = model.InOrOut.ToUpper() == "IN" ? domain.BiologType.IN : domain.BiologType.OUT,
                });
                return RedirectToAction("Index", "Home").WithInfo("Success", "Logged in.");
            }catch(Exception ex)
            {
                return View(model).WithDanger("Exception", ex.Message);
            }
        }
                                                                                                                                                                                                                                                                                                                                                      
    }
}