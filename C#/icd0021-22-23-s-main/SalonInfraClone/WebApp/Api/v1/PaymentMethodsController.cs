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
    /// PaymentMethod REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly PaymentMethodMapper _mapper;
        private readonly IAppBLL _bll;


        /// <summary>
        /// PaymentMethod controller constructor
        /// </summary>
        /// <param name="autoMapper">instance of automapper from dependency injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public PaymentMethodsController(IMapper autoMapper, IAppBLL bll)
        {
            _bll = bll;
            _mapper = new PaymentMethodMapper(autoMapper);
        }
        /// <summary>
        /// Returns list of payment methods
        /// </summary>
        /// <returns>List[PaymentMethodDTO?]</returns>
        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<List<PaymentMethodDTO?>> GetPaymentMethods()
        {
            var data = await _bll.PaymentMethodService.AllAsync();
            var res = data.Select(e => _mapper.Map(e)).ToList();
            return res!;
        }
        /*
        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
          if (_context.PaymentMethods == null)
          {
              return NotFound();
          }
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return paymentMethod;
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
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

        // POST: api/PaymentMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
          if (_context.PaymentMethods == null)
          {
              return Problem("Entity set 'ApplicationDbContext.PaymentMethods'  is null.");
          }
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            if (_context.PaymentMethods == null)
            {
                return NotFound();
            }
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentMethodExists(Guid id)
        {
            return (_context.PaymentMethods?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
