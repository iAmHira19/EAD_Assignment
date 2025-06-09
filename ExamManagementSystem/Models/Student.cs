//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//namespace ExamManagementSystem.Models
//{
//    public class Student
//    {
//        public int Id { get; set; }

//        [Required]
//        public string? Name { get; set; }

//        [Required]
//        public string? RollNumber { get; set; }

//        [Required]
//        public string? CNIC { get; set; }

//        [Required]
//        public int? SectionId { get; set; }

//        [ForeignKey("SectionId")]
//        public Section? Section { get; set; }

//        public int CourseId { get; set; }

//        [ForeignKey("CourseId")]
//        public Course? Course { get; set; }

//    }
//}




// Models/Student.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the student's name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter the roll number.")]
        public string? RollNumber { get; set; }

        [Required(ErrorMessage = "Please enter the CNIC.")]
        public string? CNIC { get; set; }

        [Required(ErrorMessage = "Please enter the address.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter the age.")]
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please select a course.")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [Required(ErrorMessage = "Please select a section.")]
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public Section? Section { get; set; }
    }
}
