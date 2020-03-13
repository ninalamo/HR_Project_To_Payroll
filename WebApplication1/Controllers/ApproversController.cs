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

namespace WebApplication1.Controllers
{
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
            ViewData["Employee"] = new SelectList(Mediator.Send(new GetEmployees_Request { PageNumber = 1, PageSize = 1000}).Result.Data.Where(i => i.CanApprove), "CompanyEmail", "FullName");
            return View();
        }

        // POST: Approvers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyEmail,Level,TypeOfRequest")] CreateApproverViewModel model)
        {
            var list = new SelectList(Mediator.Send(new GetEmployees_Request { PageNumber = 1, PageSize = 1000 }).Result.Data, "CompanyEmail", "FullName");

            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(new CreateApprover_Request
                    {
                        CompanyEmail = model.CompanyEmail,
                        Level = model.Level,
                        TypeOfRequest = model.TypeOfRequest,
                        CreatedBy = User.Identity.Name,
                        ModifiedBy = User.Identity.Name
                    });

                    //if successfully added as an approver
                    //TODO: add claims
                    //var user = await UserManager.GetUserAsync(User);
                    //await UserManager.AddToRoleAsync(user, "approver");
                    //await UserManager.AddClaimAsync(user, new Claim($"Approver_{model.TypeOfRequest.ToString()}", model.Level.ToString() ));

                    return RedirectToAction(nameof(Index)).WithSuccess("Success", "Added approver");
                }catch(Exception ex)
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
                    await Mediator.Send(new UpdateApprover_Request
                    {
                        ApproverID = model.ApproverID,
                        IsActive = model.IsActive,
                        Level = model.Level,
                        TypeOfRequest = model.TypeOfRequest
                    });

                    //TODO: auth
                    return RedirectToAction(nameof(Index)).WithSuccess("Success", "Updated approver settings.");
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

       
    }
}
