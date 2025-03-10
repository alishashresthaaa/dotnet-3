using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [MaxLength(50, ErrorMessage = "Job Title cannot exceed 150 characters")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, 1000000, ErrorMessage = "Salary must be between 0 and 1,000,000")]
        public double Salary { get; set; }

        public string Slug =>
            FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.Replace(' ', '-').ToLower();
    }
}
