using BelanjaYuk.API.Data;
using BelanjaYuk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public TransactionController(BelanjaYukContext context)
        {
            _context = context;
        }

        // POST: api/Transaction/checkout/{userId}
        [HttpPost("checkout/{userId}")]
        public async Task<IActionResult> Checkout(
            string userId,
            [FromBody] CheckoutRequest request)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new
                {
                    message = "User tidak valid."
                });
            }

            if (string.IsNullOrWhiteSpace(request.IdPayment))
            {
                return BadRequest(new
                {
                    message = "Metode pembayaran wajib dipilih."
                });
            }

            var paymentExists = await _context.LtPayment
                .AnyAsync(x =>
                    x.IdPayment == request.IdPayment &&
                    x.IsActive == true);

            if (!paymentExists)
            {
                return BadRequest(new
                {
                    message = "Metode pembayaran tidak valid."
                });
            }

            var cartItems = await _context.TrBuyerCart
                .Where(x =>
                    x.IdUser == userId &&
                    x.IsActive == true)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return BadRequest(new
                {
                    message = "Cart masih kosong."
                });
            }

            decimal finalPrice = 0m;

            var transactionId =
                $"TRX{DateTime.Now:yyyyMMddHHmmssfff}";

            var transaction = new TrBuyerTransaction
            {
                IdBuyerTransaction = transactionId,
                IdUser = userId,
                IdPayment = request.IdPayment,
                DateIn = DateTime.Now,
                UserIn = userId,
                IsActive = true
            };

            _context.TrBuyerTransaction.Add(transaction);

            foreach (var cart in cartItems)
            {
                if (string.IsNullOrEmpty(cart.IdProduct))
                {
                    continue;
                }

                var product = await _context.MsProduct
                    .FirstOrDefaultAsync(x =>
                        x.IdProduct == cart.IdProduct &&
                        x.IsActive == true);

                if (product == null)
                {
                    continue;
                }

                var qty = cart.Qty ?? 0;

                if (qty <= 0)
                {
                    continue;
                }

                var price = product.Price ?? 0m;
                var discount = product.Discount ?? 0m;
                var productFinalPrice = price - discount;
                var subtotal = productFinalPrice * qty;

                finalPrice += subtotal;

                var detail = new TrBuyerTransactionDetail
                {
                    IdBuyerTransactionDetail =
                        Guid.NewGuid().ToString(),

                    IdBuyerTransaction = transactionId,
                    IdProduct = product.IdProduct,
                    Qty = qty,
                    PriceOfProduct = price,
                    DiscountProduct = discount,
                    DateIn = DateTime.Now,
                    UserIn = userId,
                    IsActive = true
                };

                _context.TrBuyerTransactionDetail.Add(detail);

                cart.IsActive = false;
                cart.DateUp = DateTime.Now;
                cart.UserUp = userId;
            }

            transaction.FinalPrice = finalPrice;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Checkout berhasil.",
                transaction = new
                {
                    transaction.IdBuyerTransaction,
                    transaction.IdUser,
                    transaction.IdPayment,
                    transaction.FinalPrice,
                    transaction.DateIn
                }
            });
        }

        // GET: api/Transaction/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTransactions(
            string userId)
        {
            var transactions = await _context.TrBuyerTransaction
                .Where(x =>
                    x.IdUser == userId &&
                    x.IsActive == true)
                .OrderByDescending(x => x.DateIn)
                .ToListAsync();

            var result = new List<TransactionHistoryResponse>();

            foreach (var transaction in transactions)
            {
                var payment = await _context.LtPayment
                    .FirstOrDefaultAsync(x =>
                        x.IdPayment == transaction.IdPayment &&
                        x.IsActive == true);

                var details =
                    await _context.TrBuyerTransactionDetail
                        .Where(x =>
                            x.IdBuyerTransaction ==
                                transaction.IdBuyerTransaction &&
                            x.IsActive == true)
                        .ToListAsync();

                var detailResult =
                    new List<TransactionDetailResponse>();

                foreach (var detail in details)
                {
                    var product = await _context.MsProduct
                        .FirstOrDefaultAsync(x =>
                            x.IdProduct == detail.IdProduct);

                    var price = detail.PriceOfProduct ?? 0m;
                    var discount = detail.DiscountProduct ?? 0m;
                    var qty = detail.Qty ?? 0;

                    var subtotal =
                        (price - discount) * qty;

                    detailResult.Add(
                        new TransactionDetailResponse
                        {
                            IdProduct = detail.IdProduct ?? "",
                            ProductName =
                                product?.ProductName ?? "Product",
                            Qty = qty,
                            Price = price,
                            Discount = discount,
                            Subtotal = subtotal
                        });
                }

                result.Add(
                    new TransactionHistoryResponse
                    {
                        IdBuyerTransaction =
                            transaction.IdBuyerTransaction,

                        IdPayment =
                            transaction.IdPayment,

                        PaymentName =
                            payment?.PaymentName
                            ?? transaction.IdPayment
                            ?? "-",

                        FinalPrice =
                            transaction.FinalPrice ?? 0m,

                        DateIn =
                            transaction.DateIn,

                        Details =
                            detailResult
                    });
            }

            return Ok(result);
        }
    }

    public class CheckoutRequest
    {
        public string? IdPayment { get; set; }
    }

    public class TransactionHistoryResponse
    {
        public string IdBuyerTransaction { get; set; } = "";
        public string? IdPayment { get; set; }
        public string PaymentName { get; set; } = "";
        public decimal FinalPrice { get; set; }
        public DateTime? DateIn { get; set; }

        public List<TransactionDetailResponse> Details { get; set; }
            = new List<TransactionDetailResponse>();
    }

    public class TransactionDetailResponse
    {
        public string IdProduct { get; set; } = "";
        public string ProductName { get; set; } = "";
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Subtotal { get; set; }
    }
}