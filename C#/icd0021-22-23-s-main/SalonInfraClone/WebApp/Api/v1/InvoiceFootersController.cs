using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.Api.v1
{
    /// <summary>
    /// InvoiceFooter REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvoiceFootersController : ControllerBase
    {
        private readonly InvoiceFooterCreateMapper _mapper;
        private readonly IAppBLL _bll;

        /// <summary>
        /// InvoiceFooters controller constructor
        /// </summary>
        /// <param name="autoMapper">instance of automapper from dependency injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public InvoiceFootersController(IMapper autoMapper, IAppBLL bll)
        {
            _bll = bll;
            _mapper = new InvoiceFooterCreateMapper(autoMapper);
        }
        /// <summary>
        /// Returns list InvoiceFooters
        /// </summary>
        /// <returns>IEnumerable[InvoiceFooterDTOCreate?]</returns>
        // GET: api/InvoiceFooters
        [HttpGet]
        public async Task<IEnumerable<InvoiceFooterDTOCreate?>> GetInvoiceFooters()
        {
            var data = await _bll.InvoiceFooterService.AllAsync();
            var res = data.Select(e => _mapper.Map(e));
            return res;
        }

        // GET: api/InvoiceFooters/5
        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceFooter>> GetInvoiceFooter(Guid id)
        {
          if (_context.InvoiceFooters == null)
          {
              return NotFound();
          }
            var invoiceFooter = await _context.InvoiceFooters.FindAsync(id);

            if (invoiceFooter == null)
            {
                return NotFound();
            }

            return invoiceFooter;
        }

        // PUT: api/InvoiceFooters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceFooter(Guid id, InvoiceFooter invoiceFooter)
        {
            if (id != invoiceFooter.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceFooter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceFooterExists(id))
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

        // POST: api/InvoiceFooters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceFooter>> PostInvoiceFooter(InvoiceFooter invoiceFooter)
        {
          if (_context.InvoiceFooters == null)
          {
              return Problem("Entity set 'ApplicationDbContext.InvoiceFooters'  is null.");
          }
            _context.InvoiceFooters.Add(invoiceFooter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceFooter", new { id = invoiceFooter.Id }, invoiceFooter);
        }

        // DELETE: api/InvoiceFooters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceFooter(Guid id)
        {
            if (_context.InvoiceFooters == null)
            {
                return NotFound();
            }
            var invoiceFooter = await _context.InvoiceFooters.FindAsync(id);
            if (invoiceFooter == null)
            {
                return NotFound();
            }

            _context.InvoiceFooters.Remove(invoiceFooter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceFooterExists(Guid id)
        {
            return (_context.InvoiceFooters?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
