using HR.Application.cqrs.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Employees;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected async Task<ShortEmployeeViewModel> GetUserShortInfo()
        {
            var employee = await _mediator.Send(new GetEmployeeByEmail_Request {  CompanyEmail = User.Identity.Name});

            return new ShortEmployeeViewModel
            {
                Email = User.Identity.Name,
                ReportsTo = employee == null ? "" : employee.ReportsTo,
                EmployeeID = employee == null ? Guid.Empty : employee.EmployeeID,
                EmployeeNumber = employee == null ? "" : employee.EmployeeNumber,
                FullName = employee == null ? "" : employee.FullName,
                //Role = string.Join(",", await UserManager.GetRolesAsync(User.Identity as IdentityUser))
            };
        }

        protected UserManager<IdentityUser> UserManager;
        public BaseController(UserManager<IdentityUser> userManager)
        {
            this.UserManager = userManager;
        }
    }
}