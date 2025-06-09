//// SeatingPlanController.cs
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using SelectPdf;
//using ExamManagementSystem.Data;
//using ExamManagementSystem.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ExamManagementSystem.Controllers
//{
//    public class SeatingPlanController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public SeatingPlanController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult Generate()
//        {
//            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name");
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name");
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
//            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name");

//            return View(new SeatingViewModel());
//        }

//        [HttpPost]
//        public IActionResult Generate(SeatingViewModel model)
//        {
//            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name", model.BatchId);
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", model.SectionId);
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", model.CourseId);
//            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name", model.RoomId);

//            if (!_context.Batches.Any(b => b.Id == model.BatchId))
//            {
//                ModelState.AddModelError("BatchId", "Selected batch does not exist.");
//                return View(model);
//            }

//            if (!_context.Sections.Any(s => s.Id == model.SectionId))
//            {
//                ModelState.AddModelError("SectionId", "Selected section does not exist.");
//                return View(model);
//            }

//            // Get students in selected section
//            var students = _context.Students
//                .Where(s => s.SectionId == model.SectionId)
//                .OrderBy(s => s.RollNumber)
//                .ToList();

//            foreach (var student in students)
//            {
//                var plan = new SeatingPlan
//                {
//                    StudentId = student.Id,
//                    CourseId = model.CourseId,
//                    RoomId = model.RoomId,
//                    BatchId = model.BatchId,
//                    SectionId = model.SectionId,
//                    ExamDate = model.ExamDate,
//                    StartTime = model.StartTime,
//                    Duration = model.Duration
//                };
//                _context.SeatingPlans.Add(plan);
//            }

//            _context.SaveChanges();

//            model.Students = students;
//            TempData["Message"] = "Seating plan generated and saved.";
//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult ExportPdf(SeatingViewModel model)
//        {
//            var students = _context.Students
//                .Where(s => s.SectionId == model.SectionId)
//                .OrderBy(s => s.RollNumber)
//                .ToList();

//            var batchName = _context.Batches.FirstOrDefault(b => b.Id == model.BatchId)?.Name ?? "N/A";
//            var sectionName = _context.Sections.FirstOrDefault(s => s.Id == model.SectionId)?.Name ?? "N/A";
//            var roomName = _context.Rooms.FirstOrDefault(r => r.Id == model.RoomId)?.Name ?? "N/A";

//            var html = $@"
//                <h2 style='text-align:center;'>Seating Plan</h2>
//                <p><strong>Batch:</strong> {batchName}</p>
//                <p><strong>Section:</strong> {sectionName}</p>
//                <p><strong>Room:</strong> {roomName}</p>
//                <p><strong>Exam Date:</strong> {model.ExamDate:dd/MM/yyyy}</p>

//                <table border='1' cellspacing='0' cellpadding='5' width='100%'>
//                    <thead>
//                        <tr>
//                            <th>Seat No</th>
//                            <th>Student Name</th>
//                            <th>Roll Number</th>
//                            <th>Signature</th>
//                        </tr>
//                    </thead>
//                    <tbody>";

//            int seatNo = 1;
//            foreach (var s in students)
//            {
//                html += $"<tr><td>{seatNo++}</td><td>{s.Name}</td><td>{s.RollNumber}</td><td></td></tr>";
//            }

//            html += "</tbody></table>";

//            HtmlToPdf converter = new HtmlToPdf();
//            PdfDocument doc = converter.ConvertHtmlString(html);
//            byte[] pdf = doc.Save();
//            doc.Close();

//            return File(pdf, "application/pdf", "SeatingPlan.pdf");
//        }

//        [HttpGet]
//        public IActionResult GetStudentsBySection(int sectionId)
//        {
//            var students = _context.Students
//                .Where(s => s.SectionId == sectionId)
//                .OrderBy(s => s.RollNumber)
//                .Select(s => new { s.RollNumber, s.Name })
//                .ToList();

//            return Json(students);
//        }

//    }
//}









//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using SelectPdf;
//using ExamManagementSystem.Data;
//using ExamManagementSystem.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ExamManagementSystem.Controllers
//{
//    public class SeatingPlanController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public SeatingPlanController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Generate Seating Plan View (GET request)
//        [HttpGet]
//        public IActionResult Generate()
//        {
//            // Populate dropdown lists
//            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name");
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name");
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
//            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name");

//            // Initialize the model
//            return View(new SeatingViewModel());
//        }

//        // POST: Generate Seating Plan and Save to DB (POST request)
//        [HttpPost]
//        public IActionResult Generate(SeatingViewModel model)
//        {
//            // Re-populate dropdown lists with selected values
//            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name", model.BatchId);
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", model.SectionId);
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", model.CourseId);
//            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name", model.RoomId);

//            // Validation checks for Batch and Section existence
//            if (!_context.Batches.Any(b => b.Id == model.BatchId))
//            {
//                ModelState.AddModelError("BatchId", "Selected batch does not exist.");
//                return View(model);
//            }

//            if (!_context.Sections.Any(s => s.Id == model.SectionId))
//            {
//                ModelState.AddModelError("SectionId", "Selected section does not exist.");
//                return View(model);
//            }

//            // Get students in the selected section
//            var students = _context.Students
//                .Where(s => s.SectionId == model.SectionId)
//                .OrderBy(s => s.RollNumber)
//                .ToList();

//            // Try-catch for duplicate room booking errors
//            try
//            {
//                foreach (var student in students)
//                {
//                    // Check if the student already has a seating plan for this course/date/time
//                    bool alreadyExists = _context.SeatingPlans.Any(sp =>
//                        sp.StudentId == student.Id &&
//                        sp.CourseId == model.CourseId &&
//                        sp.ExamDate == model.ExamDate &&
//                        sp.StartTime == model.StartTime);

//                    if (alreadyExists) continue;  // Skip if the plan already exists

//                    // Room conflict check
//                    bool roomConflict = _context.SeatingPlans.Any(sp =>
//                        sp.RoomId == model.RoomId &&
//                        sp.ExamDate == model.ExamDate &&
//                        sp.StartTime == model.StartTime);

//                    if (roomConflict)
//                    {
//                        ModelState.AddModelError("", "❌ This room is already booked for the selected date and time.");
//                        return View(model);
//                    }

//                    // Add seating plan for the student
//                    var plan = new SeatingPlan
//                    {
//                        StudentId = student.Id,
//                        CourseId = model.CourseId,
//                        RoomId = model.RoomId,
//                        BatchId = model.BatchId,
//                        SectionId = model.SectionId,
//                        ExamDate = model.ExamDate,
//                        StartTime = model.StartTime,
//                        Duration = model.Duration
//                    };

//                    _context.SeatingPlans.Add(plan);
//                }

//                // Save changes to the database
//                _context.SaveChanges();

//                // Display success message
//                model.Students = students;
//                TempData["Message"] = "✅ Seating plan generated and saved successfully.";
//            }
//            catch (DbUpdateException ex)
//            {
//                // Handle errors gracefully (e.g., constraint violations)
//                ModelState.AddModelError("", "⚠️ A conflict occurred while saving. This may be due to room double-booking or another constraint violation.");
//                Console.WriteLine($"Error: {ex.InnerException?.Message}");
//            }

//            // Return the model with any errors or success message
//            return View(model);
//        }

//        // Export Seating Plan to PDF (POST request)
//        [HttpPost]
//        public IActionResult ExportPdf(SeatingViewModel model)
//        {
//            var students = _context.Students
//                .Where(s => s.SectionId == model.SectionId)
//                .OrderBy(s => s.RollNumber)
//                .ToList();

//            // Fetch related data for PDF generation
//            var batchName = _context.Batches.FirstOrDefault(b => b.Id == model.BatchId)?.Name ?? "N/A";
//            var sectionName = _context.Sections.FirstOrDefault(s => s.Id == model.SectionId)?.Name ?? "N/A";
//            var roomName = _context.Rooms.FirstOrDefault(r => r.Id == model.RoomId)?.Name ?? "N/A";

//            // Create HTML content for the PDF
//            var html = $@"
//                <h2 style='text-align:center;'>Seating Plan</h2>
//                <p><strong>Batch:</strong> {batchName}</p>
//                <p><strong>Section:</strong> {sectionName}</p>
//                <p><strong>Room:</strong> {roomName}</p>
//                <p><strong>Exam Date:</strong> {model.ExamDate:dd/MM/yyyy}</p>

//                <table border='1' cellspacing='0' cellpadding='5' width='100%'>
//                    <thead>
//                        <tr>
//                            <th>Seat No</th>
//                            <th>Student Name</th>
//                            <th>Roll Number</th>
//                            <th>Signature</th>
//                        </tr>
//                    </thead>
//                    <tbody>";

//            // Add each student to the seating plan in the PDF
//            int seatNo = 1;
//            foreach (var s in students)
//            {
//                html += $"<tr><td>{seatNo++}</td><td>{s.Name}</td><td>{s.RollNumber}</td><td></td></tr>";
//            }

//            html += "</tbody></table>";

//            // Convert HTML to PDF using SelectPdf
//            HtmlToPdf converter = new HtmlToPdf();
//            PdfDocument doc = converter.ConvertHtmlString(html);
//            byte[] pdf = doc.Save();
//            doc.Close();

//            // Return the generated PDF file
//            return File(pdf, "application/pdf", "SeatingPlan.pdf");
//        }

//        // AJAX: Get students by section (GET request)
//        [HttpGet]
//        public IActionResult GetStudentsBySection(int sectionId)
//        {
//            var students = _context.Students
//                .Where(s => s.SectionId == sectionId)
//                .OrderBy(s => s.RollNumber)using Microsoft.AspNetCore.Mvc;
//                .Select(s => new { s.RollNumber, s.Name })
//                .ToList();

//            return Json(students);
//        }
//    }
//}





















//using ExamManagementSystem.Data;
//using ExamManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SelectPdf;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;

//namespace ExamManagementSystem.Controllers
//{
//    public class SeatingPlanController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public SeatingPlanController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Generate Seating Plan View (GET request)
//        [HttpGet]
//        public IActionResult Generate()
//        {
//            // Populate dropdown lists
//            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name");
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name");
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
//            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name");

//            // Initialize the model
//            return View(new SeatingViewModel());
//        }

//        // POST: Generate Seating Plan and Save to DB (POST request)
//        [HttpPost]
//        public IActionResult Generate(SeatingViewModel model)
//        {
//            // Re-populate dropdown lists with selected values
//            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name", model.BatchId);
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", model.SectionId);
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", model.CourseId);
//            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name", model.RoomId);

//            // Validation checks for Batch and Section existence
//            if (!_context.Batches.Any(b => b.Id == model.BatchId))
//            {
//                ModelState.AddModelError("BatchId", "Selected batch does not exist.");
//                return View(model);
//            }

//            if (!_context.Sections.Any(s => s.Id == model.SectionId))
//            {
//                ModelState.AddModelError("SectionId", "Selected section does not exist.");
//                return View(model);
//            }

//            // Get students in selected section
//            var students = _context.Students
//                .Where(s => s.SectionId == model.SectionId)
//                .OrderBy(s => s.RollNumber)
//                .ToList();

//            try
//            {
//                foreach (var student in students)
//                {
//                    // Check if the student already has a seating plan for this course/date/time
//                    var alreadyExists = _context.SeatingPlans.Any(sp =>
//                        sp.StudentId == student.Id &&
//                        sp.CourseId == model.CourseId &&
//                        sp.ExamDate == model.ExamDate &&
//                        sp.StartTime == model.StartTime);

//                    if (alreadyExists) continue;  // Skip if the plan already exists

//                    // Room conflict check (if the room is already booked at the same time)
//                    bool roomConflict = _context.SeatingPlans.Any(sp =>
//                        sp.RoomId == model.RoomId &&
//                        sp.ExamDate == model.ExamDate &&
//                        sp.StartTime == model.StartTime);

//                    if (roomConflict)
//                    {
//                        ModelState.AddModelError("", "❌ This room is already booked for the selected date and time.");
//                        return View(model);  // Return to the view if a room conflict is found
//                    }

//                    // Add seating plan for the student
//                    var seatingPlan = new SeatingPlan
//                    {
//                        StudentId = student.Id,
//                        CourseId = model.CourseId,
//                        RoomId = model.RoomId,
//                        BatchId = model.BatchId,
//                        SectionId = model.SectionId,
//                        ExamDate = model.ExamDate,
//                        StartTime = model.StartTime,
//                        Duration = model.Duration
//                    };

//                    _context.SeatingPlans.Add(seatingPlan);
//                }

//                // Save changes to the database (commit all seating plans to the database)
//                _context.SaveChanges();

//                // Display success message
//                model.Students = students;
//                TempData["Message"] = "✅ Seating plan generated and saved successfully.";
//            }
//            catch (DbUpdateException ex)
//            {
//                // Handle errors gracefully (e.g., constraint violations)
//                ModelState.AddModelError("", "⚠️ A conflict occurred while saving. This may be due to room double-booking or another constraint violation.");
//                Console.WriteLine($"Error: {ex.Message}");  // Log the error for debugging
//                if (ex.InnerException != null)
//                {
//                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
//                }
//            }

//            // Return the model with any errors or success message
//            return View(model);
//        }

//        // Export Seating Plan to PDF (POST request)
//        [HttpPost]
//        public IActionResult ExportPdf(SeatingViewModel model)
//        {
//            var students = _context.Students
//                .Where(s => s.SectionId == model.SectionId)
//                .OrderBy(s => s.RollNumber)
//                .ToList();

//            // Fetch related data for PDF generation
//            var batchName = _context.Batches.FirstOrDefault(b => b.Id == model.BatchId)?.Name ?? "N/A";
//            var sectionName = _context.Sections.FirstOrDefault(s => s.Id == model.SectionId)?.Name ?? "N/A";
//            var roomName = _context.Rooms.FirstOrDefault(r => r.Id == model.RoomId)?.Name ?? "N/A";

//            // Create HTML content for the PDF
//            var html = $@"
//                <h2 style='text-align:center;'>Seating Plan</h2>
//                <p><strong>Batch:</strong> {batchName}</p>
//                <p><strong>Section:</strong> {sectionName}</p>
//                <p><strong>Room:</strong> {roomName}</p>
//                <p><strong>Exam Date:</strong> {model.ExamDate:dd/MM/yyyy}</p>

//                <table border='1' cellspacing='0' cellpadding='5' width='100%'>
//                    <thead>
//                        <tr>
//                            <th>Seat No</th>
//                            <th>Student Name</th>
//                            <th>Roll Number</th>
//                            <th>Signature</th>
//                        </tr>
//                    </thead>
//                    <tbody>";

//            // Add each student to the seating plan in the PDF
//            int seatNo = 1;
//            foreach (var s in students)
//            {
//                html += $"<tr><td>{seatNo++}</td><td>{s.Name}</td><td>{s.RollNumber}</td><td></td></tr>";
//            }

//            html += "</tbody></table>";

//            // Convert HTML to PDF using SelectPdf
//            HtmlToPdf converter = new HtmlToPdf();
//            PdfDocument doc = converter.ConvertHtmlString(html);
//            byte[] pdf = doc.Save();
//            doc.Close();

//            // Return the generated PDF file
//            return File(pdf, "application/pdf", "SeatingPlan.pdf");
//        }

//        // AJAX: Get students by section (GET request)
//        [HttpGet]
//        public IActionResult GetStudentsBySection(int sectionId)
//        {
//            var students = _context.Students
//                .Where(s => s.SectionId == sectionId)
//                .OrderBy(s => s.RollNumber)
//                .Select(s => new { s.RollNumber, s.Name })
//                .ToList();

//            return Json(students);
//        }
//    }
//}








using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamManagementSystem.Controllers
{
    public class SeatingPlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeatingPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Generate Seating Plan View (GET request)
        [HttpGet]
        public IActionResult Generate()
        {
            // Populate dropdown lists
            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name");
            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name");
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name");

            // Initialize the model
            return View(new SeatingViewModel());
        }

        // POST: Generate Seating Plan and Save to DB (POST request)
        //[HttpPost]
        //public IActionResult Generate(SeatingViewModel model)
        //{
        //    // Re-populate dropdown lists with selected values
        //    ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name", model.BatchId);
        //    ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", model.SectionId);
        //    ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", model.CourseId);
        //    ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name", model.RoomId);

        //    // Validate room conflict before processing
        //    bool roomConflict = _context.SeatingPlans.Any(sp =>
        //        sp.RoomId == model.RoomId &&
        //        sp.ExamDate == model.ExamDate &&
        //        sp.StartTime == model.StartTime);

        //    if (roomConflict)
        //    {
        //        ModelState.AddModelError("", "❌ This room is already booked for the selected date and time.");
        //        return View(model);
        //    }

        //    // Bulk insertion for better performance
        //    var students = _context.Students
        //        .Where(s => s.SectionId == model.SectionId)
        //        .ToList();

        //    var existingPlans = _context.SeatingPlans
        //        .Where(sp => sp.CourseId == model.CourseId &&
        //                     sp.ExamDate == model.ExamDate &&
        //                     sp.StartTime == model.StartTime &&
        //                     sp.RoomId == model.RoomId)
        //        .Select(sp => sp.StudentId)
        //        .ToHashSet();

        //    var newSeatingPlans = students
        //        .Where(s => !existingPlans.Contains(s.Id))
        //        .Select(s => new SeatingPlan
        //        {
        //            StudentId = s.Id,
        //            CourseId = model.CourseId,
        //            RoomId = model.RoomId,
        //            BatchId = model.BatchId,
        //            SectionId = model.SectionId,
        //            ExamDate = model.ExamDate,
        //            StartTime = model.StartTime,
        //            Duration = model.Duration
        //        })
        //        .ToList();

        //    try
        //    {
        //        _context.SeatingPlans.AddRange(newSeatingPlans);
        //        _context.SaveChanges();

        //        TempData["Message"] = "✅ Seating plan generated and saved successfully.";
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        ModelState.AddModelError("", "⚠️ A conflict occurred while saving. This may be due to room double-booking or another constraint violation.");
        //        Console.WriteLine($"Error: {ex.Message}");
        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
        //        }
        //    }

        //    return View(model);
        //}



        [HttpPost]
        public IActionResult Generate(SeatingViewModel model)
        {
            // Re-populate dropdown lists with selected values
            ViewBag.Batches = new SelectList(_context.Batches, "Id", "Name", model.BatchId);
            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", model.SectionId);
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", model.CourseId);
            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "Name", model.RoomId);

            // Validate room conflict before processing
            bool roomConflict = _context.SeatingPlans.Any(sp =>
                sp.RoomId == model.RoomId &&
                sp.ExamDate == model.ExamDate &&
                sp.StartTime == model.StartTime);

            if (roomConflict)
            {
                ModelState.AddModelError("", "❌ This room is already booked for the selected date and time.");
                return View(model);
            }

       
            //var existingPlans = _context.SeatingPlans
            //    .Where(sp => sp.CourseId == model.CourseId &&
            //                 sp.ExamDate == model.ExamDate &&
            //                 sp.StartTime == model.StartTime &&
            //                 sp.RoomId == model.RoomId)
            //    .Select(sp => sp.StudentId)
            //    .ToHashSet();

            var newSeatingPlans =
                new SeatingPlan{
                    CourseId = model.CourseId,
                    RoomId = model.RoomId,
                    BatchId = model.BatchId,
                    SectionId = model.SectionId,
                    ExamDate = model.ExamDate,
                    StartTime = model.StartTime,
                    Duration = model.Duration
                };

            try
            {
                _context.Add(newSeatingPlans);
                _context.SaveChanges();

                TempData["Message"] = "✅ Seating plan generated and saved successfully.";
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "⚠️ A conflict occurred while saving. This may be due to room double-booking or another constraint violation.");
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                }
            }

            return View(model);
        }
        // Export Seating Plan to PDF (POST request)
        [HttpPost]
        public IActionResult ExportPdf(SeatingViewModel model)
        {
            var students = _context.Students
                .Where(s => s.SectionId == model.SectionId)
                .OrderBy(s => s.RollNumber)
                .ToList();

            // Fetch related data for PDF generation
            var batchName = _context.Batches.FirstOrDefault(b => b.Id == model.BatchId)?.Name ?? "N/A";
            var sectionName = _context.Sections.FirstOrDefault(s => s.Id == model.SectionId)?.Name ?? "N/A";
            var roomName = _context.Rooms.FirstOrDefault(r => r.Id == model.RoomId)?.Name ?? "N/A";

            // Create HTML content for the PDF
            var html = $"<h2 style='text-align:center;'>Seating Plan</h2><p><strong>Batch:</strong> {batchName}</p><p><strong>Section:</strong> {sectionName}</p><p><strong>Room:</strong> {roomName}</p><p><strong>Exam Date:</strong> {model.ExamDate:dd/MM/yyyy}</p><table border='1' cellspacing='0' cellpadding='5' width='100%'><thead><tr><th>Seat No</th><th>Student Name</th><th>Roll Number</th><th>Signature</th></tr></thead><tbody>";

            int seatNo = 1;
            foreach (var s in students)
            {
                html += $"<tr><td>{seatNo++}</td><td>{s.Name}</td><td>{s.RollNumber}</td><td></td></tr>";
            }

            html += "</tbody></table>";

            // Convert HTML to PDF using SelectPdf
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(html);
            byte[] pdf = doc.Save();
            doc.Close();

            // Return the generated PDF file
            return File(pdf, "application/pdf", "SeatingPlan.pdf");
        }
    }
}
