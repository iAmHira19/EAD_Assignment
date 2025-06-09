// SeatingViewModel.cs
using System;
using System.Collections.Generic;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Models
{
    public class SeatingViewModel
    {
        public int BatchId { get; set; }
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }

        public DateTime ExamDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Duration { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
