using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }  // ✅ Make this optional (nullable)

        [Required]
        public string Role { get; set; }
    }
}
