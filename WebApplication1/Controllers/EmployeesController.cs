using HR.Application.cqrs.Employee.Commands;
using HR.Application.cqrs.Employee.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Extensions;
using WebApplication1.Models.Biologs;
using WebApplication1.Models.Employees;
using lib.common;
using Microsoft.AspNetCore.Mvc.Rendering;
using HR.Application.cqrs.Approver.Queries;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize(Roles ="superadmin")]
    public class EmployeesController : BaseController
    {
        public EmployeesController(UserManager<IdentityUser> userManager) : base(userManager)
        {
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetEmployees_Request());
            return View(result);
        }

        #region DTR

        public IActionResult DailyTimeRecord(Guid id)
        { 

            var employee = Mediator.Send(new GetEmployeeByIDRequest{ EmployeeID = id}).Result;
            if (employee == null) return RedirectToAction(nameof(Index)).WithWarning("Error", "Invalid ID");

            ViewData["EmployeeID"] = employee.EmployeeID;
            ViewData["FullName"] = employee.FullName;
            ViewData["EmployeeNumber"] = employee.EmployeeNumber;
            ViewData["DTR"] = new List<DailyTimeRecordResponseViewModel>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DailyTimeRecord(DailyTimeRecordRequestViewModel request)
        {
            try
            {
                var response = await Mediator.Send(new GetEmployeeBiologsByDateRange_Request { Date1 = request.Date1.Date, Date2 = request.Date2, EmployeeID = request.EmployeeID });

                var dtr = response.EmployeeTimeRecords.Select(i => new DailyTimeRecordResponseViewModel
                {
                    EmployeeNumber = i.EmployeeNumber,
                    FullName = i.FullName,
                    Lat = i.Lat,
                    Long = i.Long,
                    Mode = i.Mode,
                    Time = i.Time,
                    Location = i.Location
                }).ToList();

                ViewData["EmployeeID"] = request.EmployeeID;
                ViewData["FullName"] = request.FullName;
                ViewData["EmployeeNumber"] = request.EmployeeNumber;
                ViewData["DTR"] = dtr;

                return View(request).WithSuccess("Success", "Showing logs for DTR");
            }catch(Exception ex)
            {
              
            }
            var employee = Mediator.Send(new GetEmployeeByIDRequest { EmployeeID = request.EmployeeID }).Result;
            if (employee == null) return RedirectToAction(nameof(Index)).WithWarning("Error", "Invalid ID");

            ViewData["EmployeeID"] = employee.EmployeeID;
            ViewData["FullName"] = employee.FullName;
            ViewData["EmployeeNumber"] = employee.EmployeeNumber;
            ViewData["DTR"] = new List<DailyTimeRecordResponseViewModel>();

            return View(request).WithWarning("Warning", "Something's off.");
        }

        #endregion


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var employee = await _context.Employees
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (employee == null)
            //{
            //    return NotFound();
            //}

            //return View(employee);
            return View();
        }

        #region CRUD
        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Approvers"]  = GetApprovers();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber,FirstName,LastName,CompanyEmail,PersonalEmail,ReportsTo,CanApprove")] CreateEmployeeViewModel model)
        {
            
            ViewData["Approvers"] = GetApprovers(model.ReportsTo);

            if (ModelState.IsValid)
            {
                try
                {
                    await Mediator.Send(new CreateEmployee_Request
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmployeeNumber = model.EmployeeNumber,
                        CompanyEmail = model.CompanyEmail,
                        PersonalEmail = model.PersonalEmail,
                        CreatedBy = User.Identity.Name,
                        ApproverID = model.ReportsTo,
                        CanApprove = model.CanApprove
                    });

                    return Redirect(Url.Action("Index","Employees")).WithSuccess("Success", "Added new employee record");
                }catch(SqlException ex)
                {
                    return View(model).WithDanger("Duplicate key", ex.InnerException == null ? ex.Message : $"{ex.InnerException.Message}. {ex.Message}");
                }catch(DbUpdateException ex)
                {
                    return View(model).WithDanger(nameof(DbUpdateException), "Duplicate key value found in the request. Please check required unique key values. Example Email(s) or Name(s) or Employee Number(s)");
                }catch(Exception ex)
                {
                    return View(model).WithDanger(ex.GetType().ToString(), ex.Message);
                }
            }
            return View(model);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var employee = await Mediator.Send(new GetEmployeeByIDRequest { EmployeeID = id.Value });
            if (employee == null)
            {
                return NotFound();
            }

            //get approver selected
            var approvers = Mediator.Send(new GetApproverNamesAndEmailsOnly_Request()).Result.Result.ToList();
            var boss = approvers.FirstOrDefault(i => i.Email == employee.ReportsTo);

            ViewData["Approvers"] = GetApprovers(boss == null ? 0 : boss.ApproverID);


            return View(new UpdateEmployeeViewModel { 
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeNumber = employee.EmployeeNumber,
                CompanyEmail = employee.CompanyEmail,
                PersonalEmail = employee.PersonalEmail,
                IsActive = employee.IsActive,
                ReportsTo = boss == null ? 0 : boss.ApproverID,
                CanApprove = employee.CanApprove
            });
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployeeID,EmployeeNumber,FirstName,LastName,CompanyEmail,PersonalEmail,IsActive,EmployeeID,ReportsTo,CanApprove")] UpdateEmployeeViewModel model)
        {
            if (id != model.EmployeeID)
            {
                return NotFound();
            }
            ViewData["Approvers"] = GetApprovers(model.ReportsTo);
            if (ModelState.IsValid)
            {
                try
                {
                    

                    await Mediator.Send(new UpdateEmployee_Request {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmployeeNumber = model.EmployeeNumber,
                        CompanyEmail = model.CompanyEmail,
                        PersonalEmail = model.PersonalEmail,
                        ModifiedBy = User.Identity.Name,
                        IsActive = model.IsActive,
                        EmployeeID = model.EmployeeID,
                        ReportsTo = model.ReportsTo,
                        CanApprove = model.CanApprove,
                        
                    });

                    return Redirect(Url.Action("Index", "Employees")).WithSuccess("Success", "Updated employee details");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(model.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    return View(model).WithDanger(nameof(DbUpdateException), "Duplicate key value found in the request. Please check required unique key values. Example Email(s) or Name(s) or Employee Number(s)");
                }
                catch (Exception ex)
                {
                    return View(model).WithDanger(ex.GetType().ToString(), ex.GetExceptionMessage());
                }
               
            }
            return View(model);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await Mediator.Send(new GetEmployeeByIDRequest { EmployeeID = id.Value });

            if (response == null)
            {
                return NotFound();
            }

            

            return View(new DeleteEmployeeViewModel {
                FirstName = response.FirstName,
                LastName = response.LastName,
                EmployeeNumber = response.EmployeeNumber,
                CompanyEmail = response.CompanyEmail,
                PersonalEmail = response.PersonalEmail,
                ModifiedBy = "N/A",
                IsActive = response.IsActive
            });
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await Mediator.Send(new DeleteEmployeeRequest { EmployeeID = id });
            return RedirectToAction(nameof(Index)).WithInfo("Prompt", "Successfully removed.");
        }

        #endregion



        private bool EmployeeExists(Guid id)
        {
            return Mediator.Send(new GetEmployees_Request()).Result.Data.Any(i => i.EmployeeID == id);
             
        }
    }
}
