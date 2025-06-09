using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Room Name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter Capacity.")]
        [Range(1, 500, ErrorMessage = "Capacity must be between 1 and 500.")]
        public int Capacity { get; set; }
    }
}
