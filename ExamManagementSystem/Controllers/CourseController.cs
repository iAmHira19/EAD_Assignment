// CourseController.cs (Controller)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamManagementSystem.Data;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CourseList = await _context.Courses.ToListAsync();
            return View(new Course()); // ✅ Single model instance for the form
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            return course == null ? NotFound() : View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            return course == null ? NotFound() : View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //[HttpGet]
        //public IActionResult GetCourses()
        //{
        //    var courses = _context.Courses.ToList();
        //    return Json(courses);
        //}

        [HttpPost]
        public IActionResult AddCourse([FromBody] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _context.Courses
                .Select(c => new { c.Id, c.Name })
                .ToList();

            return Json(courses);
        }

    }
}