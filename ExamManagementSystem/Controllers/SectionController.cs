using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamManagementSystem.Data;
using ExamManagementSystem.Models;

namespace ExamManagementSystem.Controllers
{
    public class SectionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SectionLIst = _context.Sections.Include(s => s.Batch);
            return View(new Section());
            //var sections = _context.Sections.Include(s => s.Batch);
            //return View(await sections.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Batches = _context.Batches.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Sections.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Debugging: Print all errors
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Output to console
                }
            }

            ViewBag.Batches = _context.Batches.ToList();
            return View(section);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            ViewBag.Batches = _context.Batches.ToList();
            return View(section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Update(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Batches = _context.Batches.ToList();
            return View(section);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var section = await _context.Sections.Include(s => s.Batch).FirstOrDefaultAsync(s => s.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetSections()
        {
            var sections = _context.Sections
                .Include(s => s.Batch)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    BatchName = s.Batch != null ? s.Batch.Name : "No Batch"
                })
                .ToList();

            return Json(sections);
        }

    }
}
