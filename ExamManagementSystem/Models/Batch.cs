using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamManagementSystem.Models
{
    public class Batch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Batch Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Year.")]
        [Range(2000, 2100, ErrorMessage = "Please enter a valid year between 2000 and 2100.")]
        public int Year { get; set; }

        // 🔥 Initialize empty list so it's never null
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}
