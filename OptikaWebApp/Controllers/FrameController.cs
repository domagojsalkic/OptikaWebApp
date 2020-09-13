using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OptikaWeb.Models;
using OptikaWeb.ViewModels;

namespace OptikaWebApp.Controllers
{
    public class FrameController : Controller
    {
        private readonly AppContext _context;
        private IWebHostEnvironment _hostEnvironment;
        private readonly List<Frame> frames;

        public FrameController(AppContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            frames = _context.Frame.ToList();
        }

        public async Task<IActionResult> List()
        {
            var appContext = _context.Frame.Include(f => f.Category);
            return View(await appContext.ToListAsync());
        }
        public async Task<IActionResult> ListByCategory(string category)
        {
            string _category = category;
            IEnumerable<Frame> frames = _context.Frame.ToList();


            if (string.IsNullOrEmpty(category))
            {
                frames = _context.Frame;
                ViewData["CategoryList"] = "Svi okviri";
            }
            else
            {
                if (string.Equals("Ray Ban", _category, StringComparison.OrdinalIgnoreCase))
                {
                    frames = _context.Frame.Where(item => item.Category.CategoryName.Equals("Ray Ban"))
                        .OrderBy(item => item.Category.CategoryName).ToList();
                    ViewData["CategoryList"] = "Ray Ban";

                }
                else if (string.Equals("D&G", _category, StringComparison.OrdinalIgnoreCase))
                {
                    frames = _context.Frame.Where(item => item.Category.CategoryName.Equals("D&G"))
                        .OrderBy(item => item.Category.CategoryName).ToList();
                    ViewData["CategoryList"] = "D&G";
                }
                else if (string.Equals("Guess", _category, StringComparison.OrdinalIgnoreCase))
                {
                    frames = _context.Frame.Where(item => item.Category.CategoryName.Equals("Guess"))
                        .OrderBy(item => item.Category.CategoryName).ToList();
                    ViewData["CategoryList"] = "Guess";
                }
            }


            return View(frames);
        }


        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FrameViewModel frameViewModel)
        {
            string stringFileName = UploadFile(frameViewModel);
            var frame = new Frame
            {
                Name = frameViewModel.Name,
                ImageUrl = stringFileName,
                CategoryId = frameViewModel.Category.CategoryId
            };

            _context.Frame.Add(frame);
            _context.SaveChanges();

            return RedirectToAction("List");

        }

        private string UploadFile(FrameViewModel frameViewModel)
        {
            string fileName = null;
            if (frameViewModel.ImageUrl != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "FrameImages");
                fileName = Guid.NewGuid().ToString() + '-' + frameViewModel.ImageUrl.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    frameViewModel.ImageUrl.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frame = await _context.Frame
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.FrameId == id);
            if (frame == null)
            {
                return NotFound();
            }

            return View(frame);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var frame = await _context.Frame.FindAsync(id);
            _context.Frame.Remove(frame);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrameExists(int id)
        {
            return _context.Frame.Any(e => e.FrameId == id);
        }
    }
}
