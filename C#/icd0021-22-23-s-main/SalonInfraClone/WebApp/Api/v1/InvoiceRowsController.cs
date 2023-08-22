/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain.App;

namespace WebApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceRowsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceRows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceRow>>> GetInvoiceRows()
        {
          if (_context.InvoiceRows == null)
          {
              return NotFound();
          }
            return await _context.InvoiceRows.ToListAsync();
        }

        // GET: api/InvoiceRows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceRow>> GetInvoiceRow(Guid id)
        {
          if (_context.InvoiceRows == null)
          {
              return NotFound();
          }
            var invoiceRow = await _context.InvoiceRows.FindAsync(id);

            if (invoiceRow == null)
            {
                return NotFound();
            }

            return invoiceRow;
        }

        // PUT: api/InvoiceRows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceRow(Guid id, InvoiceRow invoiceRow)
        {
            if (id != invoiceRow.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceRow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceRowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InvoiceRows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceRow>> PostInvoiceRow(InvoiceRow invoiceRow)
        {
          if (_context.InvoiceRows == null)
          {
              return Problem("Entity set 'ApplicationDbContext.InvoiceRows'  is null.");
          }
            _context.InvoiceRows.Add(invoiceRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceRow", new { id = invoiceRow.Id }, invoiceRow);
        }

        // DELETE: api/InvoiceRows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceRow(Guid id)
        {
            if (_context.InvoiceRows == null)
            {
                return NotFound();
            }
            var invoiceRow = await _context.InvoiceRows.FindAsync(id);
            if (invoiceRow == null)
            {
                return NotFound();
            }

            _context.InvoiceRows.Remove(invoiceRow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceRowExists(Guid id)
        {
            return (_context.InvoiceRows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}*/
