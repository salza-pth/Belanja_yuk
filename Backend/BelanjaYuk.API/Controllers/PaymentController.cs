using BelanjaYuk.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public PaymentsController(BelanjaYukContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _context.LtPayment
                .Where(x => x.IsActive == true)
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/Payments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(string id)
        {
            var payment = await _context.LtPayment
                .FirstOrDefaultAsync(x =>
                    x.IdPayment == id &&
                    x.IsActive == true);

            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Payment tidak ditemukan."
                });
            }

            return Ok(payment);
        }
    }
}