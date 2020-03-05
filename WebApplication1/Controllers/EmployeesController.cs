using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using domain;
using persistence;
using HR.Application.cqrs.Employee.Queries;
using HR.Application.cqrs.Employee.Commands;
using WebApplication1.Models.Employees;

namespace WebApplication1.Controllers
{
    public class EmployeesController : BaseController
    {
        
        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetEmployeesRequest());
            return View(result);
        }

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

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber,FirstName,LastName,CompanyEmail,PersonalEmail")] CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new CreateEmployee_Request {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmployeeNumber = model.EmployeeNumber,
                    CompanyEmail = model.CompanyEmail,
                    PersonalEmail = model.PersonalEmail,
                    CreatedBy = "N/A"
                });
                return RedirectToAction(nameof(Index));
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
            return View(new UpdateEmployeeViewModel { 
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmployeeNumber = employee.EmployeeNumber,
                CompanyEmail = employee.CompanyEmail,
                PersonalEmail = employee.PersonalEmail,
                ModifiedBy = "N/A",
                IsActive = employee.IsActive
            });
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployeeNumber,FirstName,LastName,CompanyEmail,PersonalEmail,IsActive,EmployeeID")] UpdateEmployeeViewModel model)
        {
            if (id != model.EmployeeID)
            {
                return NotFound();
            }

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
                        ModifiedBy = "N/A",
                        IsActive = model.IsActive
                    });
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
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

            return View(null);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var employee = await _context.Employees.FindAsync(id);
            //_context.Employees.Remove(employee);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return Mediator.Send(new GetEmployeesRequest()).Result.Data.Any(i => i.EmployeeID == id);
             
        }
    }
}
