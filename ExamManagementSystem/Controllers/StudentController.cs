//// Controllers/StudentController.cs
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ExamManagementSystem.Data;
//using ExamManagementSystem.Models;

//namespace ExamManagementSystem.Controllers
//{
//    public class StudentController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public StudentController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index(string search)
//        {
//            var studentsQuery = _context.Students
//                .Include(s => s.Course)
//                .Include(s => s.Section)
//                    .ThenInclude(sec => sec.Batch)
//                .AsQueryable();

//            if (!string.IsNullOrEmpty(search))
//            {
//                studentsQuery = studentsQuery.Where(s =>
//                    s.Name.Contains(search) ||
//                    s.RollNumber.Contains(search) ||
//                    s.CNIC.Contains(search) ||
//                    s.Course.Name.Contains(search));
//            }

//            var students = await studentsQuery.ToListAsync();
//            return View(students);
//        }

//        public IActionResult Create()
//        {
//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name");
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Student student)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Students.Add(student);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", student.SectionId);
//            return View(student);
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            var student = await _context.Students.FindAsync(id);
//            if (student == null) return NotFound();

//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", student.SectionId);
//            return View(student);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Student student)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Update(student);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
//            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", student.SectionId);
//            return View(student);
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var student = await _context.Students
//                .Include(s => s.Course)
//                .Include(s => s.Section)
//                .FirstOrDefaultAsync(s => s.Id == id);

//            return student == null ? NotFound() : View(student);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var student = await _context.Students.FindAsync(id);
//            if (student != null)
//            {
//                _context.Students.Remove(student);
//                await _context.SaveChangesAsync();
//            }
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}


// Controllers/StudentController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using ExamManagementSystem.Data;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search)
        {
            var studentsQuery = _context.Students
                .Include(s => s.Course)
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Batch)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                studentsQuery = studentsQuery.Where(s =>
                    s.Name.Contains(search) ||
                    s.RollNumber.Contains(search) ||
                    s.CNIC.Contains(search) ||
                    s.Course.Name.Contains(search));
            }
            ViewBag.StudentLIst = await studentsQuery.ToListAsync();
            return View(new Student());
            //var students = await studentsQuery.ToListAsync();
            //return View(students);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name");
            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", student.SectionId);
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", student.SectionId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Name", student.CourseId);
            ViewBag.Sections = new SelectList(_context.Sections, "Id", "Name", student.SectionId);
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.Section)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student == null ? NotFound() : View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportExcel(IFormFile excelFile)
        {
            if (excelFile != null && excelFile.Length > 0)
            {
                using var stream = new MemoryStream();
                await excelFile.CopyToAsync(stream);
                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed().Skip(1); // Skip header

                foreach (var row in rows)
                {
                    var student = new Student
                    {
                        Name = row.Cell(1).GetValue<string>(),
                        RollNumber = row.Cell(2).GetValue<string>(),
                        CNIC = row.Cell(3).GetValue<string>(),
                        Address = row.Cell(4).GetValue<string>(),
                        Age = row.Cell(5).GetValue<int>(),
                        CourseId = row.Cell(6).GetValue<int>(),
                        SectionId = row.Cell(7).GetValue<int>()
                    };

                    if (!string.IsNullOrWhiteSpace(student.Name))
                    {
                        _context.Students.Add(student);
                    }
                }

                await _context.SaveChangesAsync();
                TempData["Message"] = "Students imported successfully.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students
                .Include(s => s.Course)
                .Include(s => s.Section)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.RollNumber,
                    s.CNIC,
                    s.Age,
                    s.Address,
                    Course = s.Course != null ? s.Course.Name : "N/A",
                    Section = s.Section != null ? s.Section.Name : "N/A"
                })
                .ToList();

            return Json(students);
        }

    }
}
