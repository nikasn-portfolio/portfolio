using Asp.Versioning;
using BLL.Contracts.App;
using BLL.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Api.v1
{
    /// <summary>
    /// Service REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServicesController : ControllerBase
    {
        
        private readonly IAppBLL _bll;

        /// <summary>
        /// Service controller constructor
        /// </summary>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public ServicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Services
        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns>Services bll dto objects list</returns>
        [HttpGet]
        public async Task<IEnumerable<ServiceBLLDTO>> GetServices()
        {
            var x = await _bll.Service.AllAsync();
            return x;
        }
        /*
        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(Guid id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
          if (_context.Services == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Services'  is null.");
          }
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(Guid id)
        {
            return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
