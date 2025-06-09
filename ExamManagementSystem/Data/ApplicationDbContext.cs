////namespace ExamManagementSystem.Data
////{
////    public class ApplicationDbContext
////    {
////    }
////}



//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using ExamManagementSystem.Models;

//namespace ExamManagementSystem.Data
//{
//    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Batch> Batches { get; set; }
//        public DbSet<Section> Sections { get; set; }
//        public DbSet<Student> Students { get; set; }
//        public DbSet<Room> Rooms { get; set; }
//        public DbSet<SeatingPlan> SeatingPlans { get; set; }
//        public DbSet<Course> Courses { get; set; }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<SeatingPlan>()
//                .HasOne(sp => sp.Batch)
//                .WithMany()
//                .HasForeignKey(sp => sp.BatchId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<SeatingPlan>()
//                .HasOne(sp => sp.Section)
//                .WithMany()
//                .HasForeignKey(sp => sp.SectionId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<SeatingPlan>()
//                .HasOne(sp => sp.Room)
//                .WithMany()
//                .HasForeignKey(sp => sp.RoomId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<SeatingPlan>()
//    .HasOne(sp => sp.Student)
//    .WithOne()
//    .HasForeignKey<SeatingPlan>(sp => sp.StudentId)
//    .OnDelete(DeleteBehavior.Restrict);


//        }
//    }
//}













using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<SeatingPlan> SeatingPlans { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Configure SeatingPlan foreign keys with Restrict
            modelBuilder.Entity<SeatingPlan>()
                .HasOne(sp => sp.Batch)
                .WithMany()
                .HasForeignKey(sp => sp.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatingPlan>()
                .HasOne(sp => sp.Section)
                .WithMany()
                .HasForeignKey(sp => sp.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatingPlan>()
                .HasOne(sp => sp.Room)
                .WithMany()
                .HasForeignKey(sp => sp.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<SeatingPlan>()
            //    .HasOne(sp => sp.Student)
            //    .WithMany() // ✅ Allow many SeatingPlans per student
            //    .HasForeignKey(sp => sp.StudentId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatingPlan>()
                .HasOne(sp => sp.Course)
                .WithMany()
                .HasForeignKey(sp => sp.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Composite Unique Index: One seating plan per student/course/date/time
            //modelBuilder.Entity<SeatingPlan>()
            //    .HasIndex(sp => new { sp.StudentId, sp.CourseId, sp.ExamDate, sp.StartTime })
            //    .IsUnique()
            //    .HasDatabaseName("IX_SeatingPlans_StudentCourseExam");

            // ✅ Composite Unique Index: One room cannot be reused at the same time
            modelBuilder.Entity<SeatingPlan>()
                .HasIndex(sp => new { sp.RoomId, sp.ExamDate, sp.StartTime })
                .IsUnique()
                .HasDatabaseName("IX_SeatingPlans_RoomConflict");
        }
    }
}
