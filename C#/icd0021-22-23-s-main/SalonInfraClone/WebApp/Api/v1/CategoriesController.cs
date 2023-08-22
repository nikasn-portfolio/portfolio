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
    /// Categories REST Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        private readonly CategoryMapper _mapper;

        /// <summary>
        /// Categories controller constructor
        /// </summary>
        /// <param name="autoMapper">instance of automapper from dependency injection</param>
        /// <param name="bll">instance of bll layer from dependency injection</param>
        public CategoriesController(IMapper autoMapper, IAppBLL bll)
        {
            _bll = bll;
            _mapper = new CategoryMapper(autoMapper);
        }

        // GET: api/Categories
        /// <summary>
        /// Returns list of categories
        /// </summary>
        /// <returns>List of CategoryDTO</returns>
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            
            var data =  await _bll.CategoryService.AllAsync();
            var res = data.Select(e => _mapper.Map(e));
            return res;
        }

        // GET: api/Categories/5
        /// <summary>
        /// Returns category by id
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns>Category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _bll.CategoryService.FindAsync(id);
            var res = _mapper.Map(category!);
            if (res == null!)
            {
                return NotFound();
            }

            return res;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var existingCategory = await _uow.CategoryRepository.FindAsync(id);
            
            if (existingCategory != null)
            {
                _uow.CategoryRepository.Entry(existingCategory).State = EntityState.Modified;
                
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
