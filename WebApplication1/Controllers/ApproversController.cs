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
            ViewData["Employee"] = new SelectList(Mediator.Send(new GetEmployees_Request { PageNumber = 1, PageSize = 1000}).Result.Data, "CompanyEmail", "FullName");
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
                    return RedirectToAction(nameof(Index)).WithSuccess("Success", "Added approver");
                }catch(Exception ex)
                {
                    ViewData["Employee"] = list;
                    return View(model).WithDanger("Error", ex.Message);
                }
            }

            ViewData["Employee"] = list;

            return View(model);
        }

        //// GET: Approvers/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var approver = await _context.Approvers.FindAsync(id);
        //    if (approver == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "CompanyEmail", approver.EmployeeID);
        //    return View(approver);
        //}

        //// POST: Approvers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("EmployeeID,Level,TypeOfRequest,IsActive,CreatedOn,ModifiedOn,CreatedBy,ModifiedBy,ID")] Approver approver)
        //{
        //    if (id != approver.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(approver);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ApproverExists(approver.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "CompanyEmail", approver.EmployeeID);
        //    return View(approver);
        //}

        //// GET: Approvers/Delete/5
        //public async Task<IActionResult> Delete(long? id)
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

        //// POST: Approvers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var approver = await _context.Approvers.FindAsync(id);
        //    _context.Approvers.Remove(approver);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ApproverExists(long id)
        //{
        //    return _context.Approvers.Any(e => e.ID == id);
        //}
    }
}
