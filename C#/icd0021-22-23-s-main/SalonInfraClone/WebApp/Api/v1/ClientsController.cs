using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.Api.v1
{
    /// <summary>
    /// Clients REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ClientMapper _mapper;
        
        /// <summary>
        /// Clients controller constructor
        /// </summary>
        /// <param name="autoMapper">instance of automapper from dependency injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public ClientsController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new ClientMapper(autoMapper);
        }

        // GET: api/Clients
        /// <summary>
        /// Returns list of clients
        /// </summary>
        /// <returns>IEnumerable[Client]</returns>
        [HttpGet]
        public async Task<IEnumerable<ClientDTO>> GetClients()
        {
            var x = (await _bll.ClientService.AllAsync()).Select(e => _mapper.Map(e));
            return x;
        }
        
        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Saves new client
        /// </summary>
        /// <param name="client">client dto object</param>
        /// <returns>Client object and status code 200</returns>
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody]ClientDTO client)
        {
            var x = _mapper.Map(client);
            var y = _bll.ClientService.Add(x!);
            await  _bll.SaveChangesAsync();

            return Ok(y);
        }
        /*
        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(Guid id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(Guid id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
