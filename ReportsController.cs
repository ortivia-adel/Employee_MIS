using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeMIS.Data;
using EmployeeMIS.Models;

namespace EmployeeMIS.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all departments and include their employees
            var reportData = _context.Departments
                .Include(d => d.Employees)
                .ThenInclude(e => e.PerformanceReports)
                .ToList();

            return View(reportData);
        }
    }
}