using EmployeeMIS.Data;
using EmployeeMIS.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeMIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            ViewBag.TotalDepartments = await _context.Departments.CountAsync();

            // 3. ÌáÈ ÂÎÑ 5 ãæÙÝíä Êã ÅÖÇÝÊåã
            // ÇÓÊÎÏãäÇ e.Id ááÊÑÊíÈ áÃä e.HireDate ããßä ÊÚãá ãÔßáÉ áæ ãÔ ãæÌæÏÉ Ýí ÇáÜ SQL
            var recentHires = await _context.Employees
                .Include(e => e.Department)
                .OrderByDescending(e => e.Id)
                .Take(5)
                .ToListAsync();

            return View(recentHires);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}