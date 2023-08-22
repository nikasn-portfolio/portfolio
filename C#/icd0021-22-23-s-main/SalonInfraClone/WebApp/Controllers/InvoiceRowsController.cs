using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.App;

namespace WebApp.Controllers
{
    public class InvoiceRowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceRows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceRows.Include(i => i.Invoice).Include(i => i.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceRows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.InvoiceRows == null)
            {
                return NotFound();
            }

            var invoiceRow = await _context.InvoiceRows
                .Include(i => i.Invoice)
                .Include(i => i.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceRow == null)
            {
                return NotFound();
            }

            return View(invoiceRow);
        }

        // GET: InvoiceRows/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName");
            return View();
        }

        // POST: InvoiceRows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,ServiceId,Quantity,PriceOverride,Tax,Total,Id")] InvoiceRow invoiceRow)
        {
            if (ModelState.IsValid)
            {
                invoiceRow.Id = Guid.NewGuid();
                _context.Add(invoiceRow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber", invoiceRow.InvoiceId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", invoiceRow.ServiceId);
            return View(invoiceRow);
        }

        // GET: InvoiceRows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.InvoiceRows == null)
            {
                return NotFound();
            }

            var invoiceRow = await _context.InvoiceRows.FindAsync(id);
            if (invoiceRow == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber", invoiceRow.InvoiceId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", invoiceRow.ServiceId);
            return View(invoiceRow);
        }

        // POST: InvoiceRows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceId,ServiceId,Quantity,PriceOverride,Tax,Total,Id")] InvoiceRow invoiceRow)
        {
            if (id != invoiceRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceRow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceRowExists(invoiceRow.Id))
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
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceNumber", invoiceRow.InvoiceId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "ServiceName", invoiceRow.ServiceId);
            return View(invoiceRow);
        }

        // GET: InvoiceRows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.InvoiceRows == null)
            {
                return NotFound();
            }

            var invoiceRow = await _context.InvoiceRows
                .Include(i => i.Invoice)
                .Include(i => i.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceRow == null)
            {
                return NotFound();
            }

            return View(invoiceRow);
        }

        // POST: InvoiceRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.InvoiceRows == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InvoiceRows'  is null.");
            }
            var invoiceRow = await _context.InvoiceRows.FindAsync(id);
            if (invoiceRow != null)
            {
                _context.InvoiceRows.Remove(invoiceRow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceRowExists(Guid id)
        {
          return (_context.InvoiceRows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
