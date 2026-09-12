using System.ComponentModel.DataAnnotations;

namespace EmployeeMIS.Models
{
    public class PerformanceReport
    {
        public int Id { get; set; }

        // Which employee is this about?
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        // The Report Details
        [DataType(DataType.Date)]
        [Display(Name = "Report Date")]
        public DateTime ReportDate { get; set; } = DateTime.Now;

        // The rating between 1 to 10
        [Required]
        [Display(Name = "Performance Rating (1-10)")]
        [Range(1, 10, ErrorMessage = "Please enter a value between 1 and 10")] // <--- THIS LINE ENFORCES THE LIMIT
        public int Rating { get; set; }

        // Additional Manager comments
        [Required]
        [Display(Name = "Manager Comments")]
        public string Comments { get; set; } = string.Empty; // e.g. "Mark deserves a bonus"
    }
}