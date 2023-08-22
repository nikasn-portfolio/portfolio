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
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.AppUser).Include(i => i.Company).Include(i => i.InvoiceFooter).Include(i => i.PaymentMethod).Include(i => i.Record);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.AppUser)
                .Include(i => i.Company)
                .Include(i => i.InvoiceFooter)
                .Include(i => i.PaymentMethod)
                .Include(i => i.Record)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName");
            ViewData["InvoiceFooterId"] = new SelectList(_context.InvoiceFooters, "Id", "Address");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName");
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Title");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,InvoiceFooterId,AppUserId,RecordId,PaymentMethodId,InvoiceNumber,InvoiceDate,PaymentDate,IsCompany,Comment,Id")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.Id = Guid.NewGuid();
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", invoice.AppUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", invoice.CompanyId);
            ViewData["InvoiceFooterId"] = new SelectList(_context.InvoiceFooters, "Id", "Address", invoice.InvoiceFooterId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName", invoice.PaymentMethodId);
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Title", invoice.RecordId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", invoice.AppUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", invoice.CompanyId);
            ViewData["InvoiceFooterId"] = new SelectList(_context.InvoiceFooters, "Id", "Address", invoice.InvoiceFooterId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName", invoice.PaymentMethodId);
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Title", invoice.RecordId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompanyId,InvoiceFooterId,AppUserId,RecordId,PaymentMethodId,InvoiceNumber,InvoiceDate,PaymentDate,IsCompany,Comment,Id")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", invoice.AppUserId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "CompanyName", invoice.CompanyId);
            ViewData["InvoiceFooterId"] = new SelectList(_context.InvoiceFooters, "Id", "Address", invoice.InvoiceFooterId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodName", invoice.PaymentMethodId);
            ViewData["RecordId"] = new SelectList(_context.Records, "Id", "Title", invoice.RecordId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.AppUser)
                .Include(i => i.Company)
                .Include(i => i.InvoiceFooter)
                .Include(i => i.PaymentMethod)
                .Include(i => i.Record)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(Guid id)
        {
          return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
