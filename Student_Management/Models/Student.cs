using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class Student
    {

        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Student ID is required.")]
        public string StudentId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only letters.")]
        public required string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must contain only letters.")]
        public required string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Address must contain only letters.")]
        public required string Address { get; set; }

        [Required]
        [Range(0, 4, ErrorMessage = "GPA must be between 0 and 4.")]
        public double GPA { get; set; }
    }
}
