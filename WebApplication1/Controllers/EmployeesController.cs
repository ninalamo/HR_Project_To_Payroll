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
        public async Task<IActionResult> Create([Bind("EmployeeNumber,FirstName,LastName,CompanyEmail,PersonalEmail,IsActive,CreatedOn,ModifiedOn,CreatedBy,ModifiedBy,ID")] Employee employee)
        {
            //if (ModelState.IsValid)
            //{
            //    employee.ID = Guid.NewGuid();
            //    _context.Add(employee);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            return View(null);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var employee = await _context.Employees.FindAsync(id);
            //if (employee == null)
            //{
            //    return NotFound();
            //}
            return View(null);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployeeNumber,FirstName,LastName,CompanyEmail,PersonalEmail,IsActive,CreatedOn,ModifiedOn,CreatedBy,ModifiedBy,ID")] Employee employee)
        {
            //if (id != employee.ID)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(employee);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!EmployeeExists(employee.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(employee);
            return View(null);
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
            return false;
            //return _context.Employees.Any(e => e.ID == id);
        }
    }
}
