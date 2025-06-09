using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Section
    {
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int? BatchId { get; set; }
        public Batch? Batch { get; set; }

        // 🔥 Add this line:
        public List<Student>? Students { get; set; }
    }
}
