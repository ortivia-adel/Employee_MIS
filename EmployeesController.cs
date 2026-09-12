using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Data;
using EmployeeMIS.Models;

namespace EmployeeMIS.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: Employees (قائمة الموظفين)
        public async Task<IActionResult> Index()
        {
            var employees = _context.Employees.Include(e => e.Department);
            return View(await employees.ToListAsync());
        }

        // 2. GET: Employees/Details/5 (تفاصيل موظف)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        // 3. GET: Employees/Create (فتح صفحة الإضافة)
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // 4. POST: Employees/Create (حفظ موظف جديد)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,JobTitle,HoursWorked,HourlyRate,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // 5. GET: Employees/Edit/5 (فتح صفحة التعديل)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // 6. POST: Employees/Edit/5 (حفظ التعديلات)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,JobTitle,HoursWorked,HourlyRate,DepartmentId")] Employee employee)
        {
            if (id != employee.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // 7. GET: Employees/Delete/5 (فتح صفحة الحذف)
        // GET: Employees/Delete/5 (صفحة التأكيد)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5 (التنفيذ الفعلي للحذف)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.PerformanceReports) // تضمين التقارير لمنع مشاكل Foreign Key
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                // حذف التقارير المرتبطة بالموظف أولاً إن وجدت
                if (employee.PerformanceReports != null && employee.PerformanceReports.Any())
                {
                    _context.Set<PerformanceReport>().RemoveRange(employee.PerformanceReports);
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // 9. GET: Employees/Report (تقارير الموظفين)
        public async Task<IActionResult> Report()
        {
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return View(employees);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => id == e.Id);
        }
    }
}