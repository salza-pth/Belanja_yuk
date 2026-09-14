using BelanjaYuk.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public ProductsController(BelanjaYukContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string? category)
        {
            var query = _context.MsProduct
                .AsNoTracking()
                .Where(p => p.IsActive == true);

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.IdCategory == category);
            }

            var products = await query.ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _context.MsProduct
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdProduct == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product tidak ditemukan."
                });
            }

            return Ok(product);
        }
    }
}