
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSD_Lab1EB.Data;
using SSD_Lab1EB.Models;

[Authorize(Roles = "Employee,Supervisor")]
public class ServicesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServicesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: SERVICES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Service.ToListAsync());
    }

    // GET: SERVICES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var service = await _context.Service
            .FirstOrDefaultAsync(m => m.Id == id);
        if (service == null)
        {
            return NotFound();
        }

        return View(service);
    }

    // GET: SERVICES/Create
    [Authorize(Roles = "Supervisor")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: SERVICES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Category,HourlyRate,DurationMinutes,IsAvailable")] Service service)
    {
        if (ModelState.IsValid)
        {
            _context.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(service);
    }

    // GET: SERVICES/Edit/5
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var service = await _context.Service.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return View(service);
    }

    // POST: SERVICES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description,Category,HourlyRate,DurationMinutes,IsAvailable")] Service service)
    {
        if (id != service.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(service);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(service.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(service);
    }

    // GET: SERVICES/Delete/5
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var service = await _context.Service
            .FirstOrDefaultAsync(m => m.Id == id);
        if (service == null)
        {
            return NotFound();
        }

        return View(service);
    }

    // POST: SERVICES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var service = await _context.Service.FindAsync(id);
        if (service != null)
        {
            _context.Service.Remove(service);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ServiceExists(int? id)
    {
        return _context.Service.Any(e => e.Id == id);
    }
}
