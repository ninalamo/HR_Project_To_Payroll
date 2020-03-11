using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain;
using persistence;
using application.interfaces;
using HR.Application.cqrs.Request.Queries;
using HR.Application.cqrs.Approver.Queries;
using System.Threading;
using HR.Application.cqrs.Request.Commands;
using WebApplication1.Models.Requests.Overtime;
using WebApplication1.Extensions;
using Microsoft.AspNetCore.Identity;
using HR.Application.cqrs.Employee.Queries;

namespace WebApplication1.Controllers
{
    
    public class OverTimeRequestsController : BaseController
    {
        public OverTimeRequestsController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }


        // GET: OverTimeRequests
        public async Task<IActionResult> Index()
        {
            GetOverTimeRequests_Response response = new GetOverTimeRequests_Response();

            var approvers = (await Mediator.Send(new GetApprovers_Request(), CancellationToken.None)).Approvers.Where(i => i.TypeOfRequest == RequestType.Overtime);

            var currentUser = approvers.FirstOrDefault(i => i.CompanyEmail.ToLower() == User.Identity.Name.ToLower());

            ViewData["IsApprover"] = currentUser != null;

            return View(await Mediator.Send(new GetOverTimeRequests_Request()));
        }

        // GET: OverTimeRequests/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var overTimeRequest = await _context.OverTimeRequests
        //        .Include(o => o.Tracker)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (overTimeRequest == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(overTimeRequest);
        //}

        // GET: OverTimeRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OverTimeRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStart,TimeEnd,Classification,ShiftDate,Purpose")] CreateOverTimeRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = new CreateOverTimeRequest_Request
                    {
                        Classification = model.Classification,
                        Purpose = model.Purpose,
                        ShiftDate = model.ShiftDate.Date,
                        TimeStart = model.TimeStart,
                        TimeEnd = model.TimeEnd,
                        CreatedBy = User.Identity.Name
                    };

                    var getApproversResponse = await Mediator.Send(new GetOvertimeRequestApprovers_Request());

                    if (!getApproversResponse.Approvers.Any()) throw new Exception("Setup required. Please make sure that approvers are configured.");

                    var shortInfo = await GetUserShortInfo();
                    if(string.IsNullOrEmpty(shortInfo.ReportsTo))
                        throw new Exception("Cannot find approver for employee. Please check 'ReportsTo' field in Employee profile.");

                    request.Supervisor = shortInfo.ReportsTo.ToLower();
                    int top = getApproversResponse.Approvers.Max(i => i.Level);
                    request.FinalApprover = getApproversResponse.Approvers.FirstOrDefault(i => i.Level == top).CompanyEmail;

                    request.Requestor = User.Identity.Name;

                    await Mediator.Send(request);

                    return RedirectToAction(nameof(Index)).WithSuccess("Success", "Created an overtime request.");

                }catch(Exception ex)
                {
                    return View(model).WithDanger("Error", ex.Message);
                }

            }
            return View(model);
        }

        //// GET: OverTimeRequests/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var overTimeRequest = await _context.OverTimeRequests.FindAsync(id);
        //    if (overTimeRequest == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TrackerID"] = new SelectList(_context.RequestTrackers, "ID", "ID", overTimeRequest.TrackerID);
        //    return View(overTimeRequest);
        //}

        //// POST: OverTimeRequests/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("TrackerID,EmployeeID,TimeStart,TimeEnd,Classification,ShiftDate,Purpose,IsActive,CreatedOn,ModifiedOn,CreatedBy,ModifiedBy,ID")] OverTimeRequest overTimeRequest)
        //{
        //    if (id != overTimeRequest.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(overTimeRequest);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OverTimeRequestExists(overTimeRequest.ID))
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
        //    ViewData["TrackerID"] = new SelectList(_context.RequestTrackers, "ID", "ID", overTimeRequest.TrackerID);
        //    return View(overTimeRequest);
        //}

        //// GET: OverTimeRequests/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var overTimeRequest = await _context.OverTimeRequests
        //        .Include(o => o.Tracker)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (overTimeRequest == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(overTimeRequest);
        //}

        //// POST: OverTimeRequests/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var overTimeRequest = await _context.OverTimeRequests.FindAsync(id);
        //    _context.OverTimeRequests.Remove(overTimeRequest);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OverTimeRequestExists(long id)
        //{
        //    return _context.OverTimeRequests.Any(e => e.ID == id);
        //}
    }
}
