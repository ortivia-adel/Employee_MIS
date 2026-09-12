using System.ComponentModel.DataAnnotations;

namespace EmployeeMIS.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}