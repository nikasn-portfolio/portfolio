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
    public class InvoiceFootersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceFootersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceFooters
        public async Task<IActionResult> Index()
        {
              return _context.InvoiceFooters != null ? 
                          View(await _context.InvoiceFooters.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InvoiceFooters'  is null.");
        }

        // GET: InvoiceFooters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.InvoiceFooters == null)
            {
                return NotFound();
            }

            var invoiceFooter = await _context.InvoiceFooters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceFooter == null)
            {
                return NotFound();
            }

            return View(invoiceFooter);
        }

        // GET: InvoiceFooters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceFooters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Phone,Address,Iban,CompanyName,Id")] InvoiceFooter invoiceFooter)
        {
            if (ModelState.IsValid)
            {
                invoiceFooter.Id = Guid.NewGuid();
                _context.Add(invoiceFooter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceFooter);
        }

        // GET: InvoiceFooters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.InvoiceFooters == null)
            {
                return NotFound();
            }

            var invoiceFooter = await _context.InvoiceFooters.FindAsync(id);
            if (invoiceFooter == null)
            {
                return NotFound();
            }
            return View(invoiceFooter);
        }

        // POST: InvoiceFooters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Email,Phone,Address,Iban,CompanyName,Id")] InvoiceFooter invoiceFooter)
        {
            if (id != invoiceFooter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceFooter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceFooterExists(invoiceFooter.Id))
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
            return View(invoiceFooter);
        }

        // GET: InvoiceFooters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.InvoiceFooters == null)
            {
                return NotFound();
            }

            var invoiceFooter = await _context.InvoiceFooters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceFooter == null)
            {
                return NotFound();
            }

            return View(invoiceFooter);
        }

        // POST: InvoiceFooters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.InvoiceFooters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InvoiceFooters'  is null.");
            }
            var invoiceFooter = await _context.InvoiceFooters.FindAsync(id);
            if (invoiceFooter != null)
            {
                _context.InvoiceFooters.Remove(invoiceFooter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceFooterExists(Guid id)
        {
          return (_context.InvoiceFooters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
