using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ExamManagementSystem.Data;
using ExamManagementSystem.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExamManagementSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.BatchList = await _context.Batches.ToListAsync();
            return View(new Batch());
            //var batches = await _context.Batches.ToListAsync();
            //return View(batches);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
              

                _context.Batches.Add(batch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(batch);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }
            return View(batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Batch batch)
        {
            if (id != batch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(batch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(batch);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }
            return View(batch);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch != null)
            {
                _context.Batches.Remove(batch);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult GetBatches()
        {
            var batches = _context.Batches
                .Select(b => new { b.Id, b.Name, b.Year })
                .ToList();

            return Json(batches);
        }

    }
}
