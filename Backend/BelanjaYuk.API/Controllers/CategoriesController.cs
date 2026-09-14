using BelanjaYuk.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public CategoriesController(BelanjaYukContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.LtCategory
                .AsNoTracking()
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/Categories/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveCategories()
        {
            var categories = await _context.LtCategory
                .AsNoTracking()
                .Where(c => c.IsActive == true)
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/Categories/CAT001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            var category = await _context.LtCategory
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCategory == id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Category tidak ditemukan."
                });
            }

            return Ok(category);
        }
    }
}