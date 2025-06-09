using System;

namespace ExamManagementSystem.Models
{
    public class SeatingPlan
    {
        public int Id { get; set; }

        //public int StudentId { get; set; }
        //public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public DateTime ExamDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public string Duration { get; set; }
    }
}


//using ExamManagementSystem.Models;

//public class SeatingPlan
//{
//    public int Id { get; set; }

//    public int StudentId { get; set; }
//    public int CourseId { get; set; }
//    public int RoomId { get; set; }
//    public int BatchId { get; set; }
//    public int SectionId { get; set; }

//    public DateTime ExamDate { get; set; }
//    public TimeSpan StartTime { get; set; }
//    public string? Duration { get; set; }

//    public Student? Student { get; set; }
//    public Course? Course { get; set; }
//    public Room? Room { get; set; }
//    public Batch? Batch { get; set; }
//    public Section? Section { get; set; }
//}
