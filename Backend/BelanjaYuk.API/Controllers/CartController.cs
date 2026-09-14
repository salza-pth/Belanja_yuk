using BelanjaYuk.API.Data;
using BelanjaYuk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public CartController(BelanjaYukContext context)
        {
            _context = context;
        }

        // GET: api/Cart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _context.TrBuyerCart
                .Where(x =>
                    x.IdUser == userId &&
                    x.IsActive == true)
                .OrderByDescending(x => x.DateIn)
                .ToListAsync();

            return Ok(cart);
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(
            [FromBody] CartRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.IdUser))
            {
                return BadRequest(new
                {
                    message = "User tidak valid."
                });
            }

            if (string.IsNullOrWhiteSpace(request.IdProduct))
            {
                return BadRequest(new
                {
                    message = "Product tidak valid."
                });
            }

            if (request.Qty <= 0)
            {
                request.Qty = 1;
            }

            // Pastikan product tersedia
            var product = await _context.MsProduct
                .FirstOrDefaultAsync(x =>
                    x.IdProduct == request.IdProduct &&
                    x.IsActive == true);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Product tidak ditemukan."
                });
            }

            // Cari cart berdasarkan user + product
            // Termasuk cart yang sudah tidak aktif
            var existingCart = await _context.TrBuyerCart
                .FirstOrDefaultAsync(x =>
                    x.IdUser == request.IdUser &&
                    x.IdProduct == request.IdProduct);

            if (existingCart != null)
            {
                // Kalau cart lama sudah dihapus,
                // aktifkan kembali
                if (existingCart.IsActive != true)
                {
                    existingCart.Qty = request.Qty;
                    existingCart.IsActive = true;
                    existingCart.DateUp = DateTime.Now;
                    existingCart.UserUp = request.IdUser;
                }
                else
                {
                    // Kalau masih aktif, tambahkan quantity
                    existingCart.Qty =
                        (existingCart.Qty ?? 0) + request.Qty;

                    existingCart.DateUp = DateTime.Now;
                    existingCart.UserUp = request.IdUser;
                }

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Product berhasil ditambahkan ke cart.",
                    cart = existingCart
                });
            }

            // Kalau belum pernah ada cart untuk product ini,
            // buat record baru
            var cartId = await GenerateCartId();

            var newCart = new TrBuyerCart
            {
                IdBuyerCart = cartId,
                IdUser = request.IdUser,
                IdProduct = request.IdProduct,
                Qty = request.Qty,
                DateIn = DateTime.Now,
                UserIn = request.IdUser,
                IsActive = true
            };

            _context.TrBuyerCart.Add(newCart);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product berhasil ditambahkan ke cart.",
                cart = newCart
            });
        }

        // PUT: api/Cart/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(
            string id,
            [FromBody] CartUpdateRequest request)
        {
            if (request.Qty <= 0)
            {
                return BadRequest(new
                {
                    message = "Quantity harus lebih dari 0."
                });
            }

            var cart = await _context.TrBuyerCart
                .FirstOrDefaultAsync(x =>
                    x.IdBuyerCart == id &&
                    x.IsActive == true);

            if (cart == null)
            {
                return NotFound(new
                {
                    message = "Item cart tidak ditemukan."
                });
            }

            cart.Qty = request.Qty;
            cart.DateUp = DateTime.Now;
            cart.UserUp = cart.IdUser;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Cart berhasil diperbarui.",
                cart
            });
        }

        // DELETE: api/Cart/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var cart = await _context.TrBuyerCart
                .FirstOrDefaultAsync(x =>
                    x.IdBuyerCart == id &&
                    x.IsActive == true);

            if (cart == null)
            {
                return NotFound(new
                {
                    message = "Item cart tidak ditemukan."
                });
            }

            // Soft delete
            cart.IsActive = false;
            cart.DateUp = DateTime.Now;
            cart.UserUp = cart.IdUser;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Product berhasil dihapus dari cart."
            });
        }

        private async Task<string> GenerateCartId()
        {
            var lastCart = await _context.TrBuyerCart
                .OrderByDescending(x => x.IdBuyerCart)
                .FirstOrDefaultAsync();

            if (lastCart == null ||
                string.IsNullOrWhiteSpace(lastCart.IdBuyerCart))
            {
                return "CART001";
            }

            var numberPart =
                lastCart.IdBuyerCart.Replace("CART", "");

            if (int.TryParse(numberPart, out int number))
            {
                number++;
                return $"CART{number:D3}";
            }

            return $"CART{DateTime.Now:yyyyMMddHHmmss}";
        }
    }

    public class CartRequest
    {
        public string IdUser { get; set; } = "";
        public string IdProduct { get; set; } = "";
        public int Qty { get; set; } = 1;
    }

    public class CartUpdateRequest
    {
        public int Qty { get; set; }
    }
}