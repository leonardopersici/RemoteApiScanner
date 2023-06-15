using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteApiScanner.Data;
using RemoteApiScanner.Models;

namespace RemoteApiScanner.Controllers
{
    public class EsecuzioniKiteRunnersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WorkerController> _logger;

        public EsecuzioniKiteRunnersController(ApplicationDbContext context, ILogger<WorkerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: EsecuzioniKiteRunners
        public async Task<IActionResult> Index()
        {
            return _context.EsecuzioniKiteRunners != null ?
                        View(await _context.EsecuzioniKiteRunners.Where(x => x.user == User.Identity.Name).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.EsecuzioniKiteRunners'  is null.");
        }

        // GET: EsecuzioniKiteRunners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.EsecuzioniKiteRunners == null)
            {
                return NotFound();
            }

            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners
                .FirstOrDefaultAsync(m => m.id == id);
            if (esecuzioniKiteRunner == null)
            {
                return NotFound();
            }

            return View(esecuzioniKiteRunner);
        }

        // GET: EsecuzioniKiteRunners/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: EsecuzioniKiteRunners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.EsecuzioniKiteRunners == null)
            {
                return NotFound();
            }

            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners.FindAsync(id);
            if (esecuzioniKiteRunner == null)
            {
                return NotFound();
            }
            return View(esecuzioniKiteRunner);
        }

        // POST: EsecuzioniKiteRunners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EsecuzioniKiteRunner esecuzioniKiteRunner)
        {
            if (id != esecuzioniKiteRunner.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(esecuzioniKiteRunner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EsecuzioniKiteRunnerExists(esecuzioniKiteRunner.id))
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
            return View(esecuzioniKiteRunner);
        }

        // GET: EsecuzioniKiteRunners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.EsecuzioniKiteRunners == null)
            {
                return NotFound();
            }

            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners
                .FirstOrDefaultAsync(m => m.id == id);
            if (esecuzioniKiteRunner == null)
            {
                return NotFound();
            }

            return View(esecuzioniKiteRunner);
        }

        // POST: EsecuzioniKiteRunners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.EsecuzioniKiteRunners == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EsecuzioniKiteRunners'  is null.");
            }
            var esecuzioniKiteRunner = await _context.EsecuzioniKiteRunners.FindAsync(id);
            if (esecuzioniKiteRunner != null)
            {
                _context.EsecuzioniKiteRunners.Remove(esecuzioniKiteRunner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EsecuzioniKiteRunnerExists(Guid id)
        {
            return (_context.EsecuzioniKiteRunners?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
