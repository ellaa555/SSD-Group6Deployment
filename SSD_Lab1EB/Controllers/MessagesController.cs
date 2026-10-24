using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSD_Lab1EB.Data;
using SSD_Lab1EB.Models;

namespace SSD_Lab1EB.Controllers
{
    /// <summary>
    /// Message board: authenticated users can view and post messages only.
    /// Edit and Delete are intentionally not provided.
    /// </summary>
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var messages = await _context.Message
                .OrderByDescending(m => m.PostedAt)
                .ToListAsync();

            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // Only Content is bound from the form. Author and PostedAt are set on the server.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.Author = User.Identity?.Name;
                message.PostedAt = DateTime.UtcNow;

                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(message);
        }
    }
}