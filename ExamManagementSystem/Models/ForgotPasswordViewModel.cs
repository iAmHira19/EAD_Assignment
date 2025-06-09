using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
