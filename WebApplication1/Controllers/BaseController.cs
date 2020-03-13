using HR.Application.cqrs.Approver.Queries;
using HR.Application.cqrs.Employee.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Employees;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private IMediator _mediator;
        private static ICollection<SelectListItem> _approvers;

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

        protected ICollection<SelectListItem> GetApprovers(long selected = 0)
        {
            var approvers = Mediator.Send(new GetApproverNamesAndEmailsOnly_Request()).Result.Result;
            var selectList = new SelectList(
                approvers.Where(i => i.Email != User.Identity.Name)
                .ToList(), "ApproverID", "DisplayName", selected).ToList();
            return selectList;
        }

        protected UserManager<IdentityUser> UserManager;
        public BaseController(UserManager<IdentityUser> userManager)
        {
            this.UserManager = userManager;
        }
    }
}