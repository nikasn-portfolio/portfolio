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
    /// Companies REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyMapper _mapper;
        private readonly IAppBLL _bll;

        /// <summary>
        /// Companies controller constructor
        /// </summary>
        /// <param name="autoMapper">instance of automapper from dependancy injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public CompaniesController(IMapper autoMapper, IAppBLL bll)
        {
            _bll = bll;
            _mapper = new CompanyMapper(autoMapper);
        }

        /// <summary>
        /// Returns list of companies
        /// </summary>
        /// <returns>IEnumerable[CompanyDTO?]</returns>
        // GET: api/Companies
        [HttpGet]
        public async Task<IEnumerable<CompanyDTO?>> GetCompanies()
        {
            var data = await _bll.CompanyService.AllAsync();
            var res = data.Select(e => _mapper.Map(e));
            return res;
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Saves new company
        /// </summary>
        /// <param name="company">Company dto object</param>
        /// <returns>Company object and 200 status</returns>
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany([FromBody] CompanyDTO company)
        {
            var companyAdded = _bll.CompanyService.Add(_mapper.Map(company)!);
            await _bll.SaveChangesAsync();
            return Ok(_mapper.Map(companyAdded));
        }
        /*
        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
          if (_context.Companies == null)
          {
              return NotFound();
          }
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany([FromBody]CompanyDTO company)
        {
            _uow.CompanyRepository.Add(_mapper.Map(company)!);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(Guid id)
        {
            return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}