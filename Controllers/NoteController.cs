using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UDAS.Data;
using UDAS.Entities;
using UDAS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace UDAS.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var surname = User.FindFirst(ClaimTypes.Surname)?.Value;

            var viewModel = new NoteViewModel
            {
                UserId = int.Parse(userId),
                Name = name,
                Surname = surname
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var note = new Note
            {
                UserId = model.UserId,
                Name = model.Name,
                Surname = model.Surname,
                NoteType = model.NoteType,
                Content = model.Content,
                CreatedAt = DateTime.Now
            };

            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();

            return RedirectToAction("Add");
        }

        
        [HttpGet]
        public async Task<IActionResult> MyNotes()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var notes = await _context.Notes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notes);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyNotes");
        }
    }
}
