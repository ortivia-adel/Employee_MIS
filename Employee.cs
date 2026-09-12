using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMIS.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        public string Name { get => FullName; set => FullName = value; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hourly Rate is required")]
        [Range(0, 10000, ErrorMessage = "Hourly Rate must be a positive number")]
        [Display(Name = "Hourly Rate ($)")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal HourlyRate { get; set; }

        [Required(ErrorMessage = "Hours Worked is required")]
        [Range(0, 720, ErrorMessage = "Hours worked must be between 0 and 720")]
        [Display(Name = "Hours Worked")]
        public double HoursWorked { get; set; }

        [Display(Name = "Total Salary ($)")]
        public decimal TotalSalary => (decimal)HoursWorked * HourlyRate;

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

        public virtual ICollection<PerformanceReport>? PerformanceReports { get; set; }
    }
}