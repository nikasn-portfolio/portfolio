/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Contracts.App;
using Domain.App;

namespace WebApp.Controllers
{
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppUOW _uow;

        public RecordsController(ApplicationDbContext context, IAppUOW uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Records
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Records.Include(s => s.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Records/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // GET: Records/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName");
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Title,StartTime,EndTime,IsHidden,Comment,Id")] Record record)
        {
            if (ModelState.IsValid)
            {
                record.Id = Guid.NewGuid();
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", record.ServiceId);
            return View(@record);
        }

        // GET: Records/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", record.ServiceId);
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ServiceId,Title,StartTime,EndTime,IsHidden,Comment,Id")] Record @record)
        {
            if (id != record.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.Id))
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
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", record.ServiceId);
            return View(@record);
        }

        // GET: Records/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var @record = await _context.Records
                .Include(s => s.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Records == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Records'  is null.");
            }
            var record = await _context.Records.FindAsync(id);
            if (record != null)
            {
                _context.Records.Remove(record);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(Guid id)
        {
          return (_context.Records?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}*/
