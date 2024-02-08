using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployerAplication.Data;
using EmployerAplication.Models;
using System.Text.RegularExpressions;

namespace EmployerAplication.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployerAplicationContext _context;

        public EmployeesController(EmployerAplicationContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(String searchString)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set Employer is bad ");
            }
            var Employees = from e in _context.Employee
                            select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                Employees = Employees.Where(e =>e.Name.Contains(searchString));
            }
            return View(await Employees.OrderBy(e => e.BornDate).ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,LastName,RFC,BornDate,Status")] Employee employee)
        {

            if (!IsValidRFC(employee.RFC))
            {
                ModelState.AddModelError("RFC", "Invalid Format for this RFC");
                return View(employee);
            }
            


            if (ModelState.IsValid)
            {

                if (_context.Employee.Any(e => e.RFC == employee.RFC))
                {
                    ModelState.AddModelError("RFC", "This RFC is already registered");
                    return View(employee);
                }
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,LastName,RFC,BornDate,Status")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }
            if (!IsValidRFC(employee.RFC))
            {
                ModelState.AddModelError("RFC", "Invalid Format for this RFC");
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.ID == id);
        }

        private bool IsValidRFC(string rfc)
        {
            string rfcPattern = "^[A-Z]{4}[0-9]{6}[A-Z0-9]{3}$";
            return Regex.IsMatch(rfc, rfcPattern);
        }
    }
}
