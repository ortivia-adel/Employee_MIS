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
    public class PerformanceReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformanceReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PerformanceReports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PerformanceReports.Include(p => p.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PerformanceReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceReport = await _context.PerformanceReports
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performanceReport == null)
            {
                return NotFound();
            }

            return View(performanceReport);
        }

        // GET: PerformanceReports/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email");
            return View();
        }

        // POST: PerformanceReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,ReportDate,Rating,Comments")] PerformanceReport performanceReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performanceReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", performanceReport.EmployeeId);
            return View(performanceReport);
        }

        // GET: PerformanceReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceReport = await _context.PerformanceReports.FindAsync(id);
            if (performanceReport == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", performanceReport.EmployeeId);
            return View(performanceReport);
        }

        // POST: PerformanceReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,ReportDate,Rating,Comments")] PerformanceReport performanceReport)
        {
            if (id != performanceReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performanceReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceReportExists(performanceReport.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", performanceReport.EmployeeId);
            return View(performanceReport);
        }

        // GET: PerformanceReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceReport = await _context.PerformanceReports
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performanceReport == null)
            {
                return NotFound();
            }

            return View(performanceReport);
        }

        // POST: PerformanceReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performanceReport = await _context.PerformanceReports.FindAsync(id);
            if (performanceReport != null)
            {
                _context.PerformanceReports.Remove(performanceReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceReportExists(int id)
        {
            return _context.PerformanceReports.Any(e => e.Id == id);
        }
    }
}
