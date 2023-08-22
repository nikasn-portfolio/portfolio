using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO;
using Public.DTO.InvoiceViewDTOs;
using Public.DTO.Mappers;

namespace WebApp.Api.v1
{
    /// <summary>
    /// Invoice REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceMapper _mapper;
        private readonly InvoiceOnGetMapper _mapperOnGet;
        private readonly InvoiceOnViewMapper _mapperOnView;
        private readonly IAppBLL _bll;

        /// <summary>
        /// Invoice controller constructor
        /// </summary>
        /// <param name="autoMapper">instance of automapper from dependency injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public InvoicesController(IMapper autoMapper, IAppBLL bll)
        {
            _bll = bll;
            _mapper = new InvoiceMapper(autoMapper);
            _mapperOnGet = new InvoiceOnGetMapper(autoMapper);
            _mapperOnView = new InvoiceOnViewMapper(autoMapper);
        }

        // GET: api/v1/Invoices
        /// <summary>
        /// Returns list of invoices
        /// </summary>
        /// <returns>List[InvoicesDTOForListOnGet?]</returns>
        [HttpGet]
        public async Task<List<InvoicesDTOForListOnGet?>> GetInvoice()
        {
            var data = await _bll.InvoiceService.AllInvoicesWithIncludes();
            var res = data.Select(e => _mapperOnGet.Map(e)).ToList();
            return res!;
        }

        // GET: api/v1/Invoices/5
        /// <summary>
        /// Returns invoice by id
        /// </summary>
        /// <param name="id">invoice id</param>
        /// <returns>InvoiceViewDTO?</returns>
        [HttpGet("{id}")]
        public Task<InvoiceViewDTO?> GetInvoicesById(Guid id)
        {
            Console.WriteLine("GET WITH ID");
            var data = _bll.InvoiceService.AllInvoicesWithIncludes().Result.FirstOrDefault(e => e.Id == id);
            var res =  _mapperOnView.Map(data!);
            return Task.FromResult(res)!;
        }
        /*
        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add invoice to database
        /// </summary>
        /// <param name="invoice">InvoiceDTOCreate object</param>
        /// <returns>Invoice</returns>
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoiceDTOCreate invoice)
        {
            //invoice.PaymentDate = invoice.PaymentDate;  //TimeZoneInfo.ConvertTime((DateTime)invoice.PaymentDate!, TimeZoneInfo.FindSystemTimeZoneById("E. Africa Standard Time"));
            
            var domainInvoice = _mapper.Map(invoice);
            domainInvoice!.Id = Guid.NewGuid();
            foreach (var row in domainInvoice.InvoiceRows!)
            {
                row.Id = Guid.NewGuid();
                row.InvoiceId = domainInvoice.Id;
            }
            _bll.InvoiceService.Add(domainInvoice);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(domainInvoice));
        }

        // DELETE: api/Invoices/5
        /// <summary>
        /// Delete from database invoice and sets to record status without invoice
        /// </summary>
        /// <param name="id">invoice id</param>
        /// <returns>Status code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.InvoiceService.GetInvoiceById(id);
            if (invoice == null) return NotFound();
            var record = invoice.Record;
            if (record == null) return NotFound();
            record.IsHidden = false;
            _bll.RecordService.Update(record);
            _bll.InvoiceService.Remove(invoice);
            await _bll.SaveChangesAsync();
            return Ok();
        }
    }
}
