// Course.cs (Model)
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        public string Name { get; set; }
    }
}