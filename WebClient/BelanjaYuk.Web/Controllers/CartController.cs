using BelanjaYuk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BelanjaYuk.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var response = await client.GetAsync($"api/Cart/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Gagal mengambil data cart.";
                return View(new List<CartViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();

            var cart = JsonSerializer.Deserialize<List<CartViewModel>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<CartViewModel>();

            foreach (var item in cart)
            {
                if (string.IsNullOrEmpty(item.IdProduct))
                    continue;

                var productResponse =
                    await client.GetAsync($"api/Products/{item.IdProduct}");

                if (!productResponse.IsSuccessStatusCode)
                    continue;

                var productJson =
                    await productResponse.Content.ReadAsStringAsync();

                var product =
                    JsonSerializer.Deserialize<ProductViewModel>(
                        productJson,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (product != null)
                {
                    item.ProductName = product.ProductName;
                    item.Price = product.Price;
                    item.Discount = product.Discount;
                }
            }

            // Ambil payment aktif
            var paymentResponse =
                await client.GetAsync("api/Payments");

            var payments = new List<PaymentViewModel>();

            if (paymentResponse.IsSuccessStatusCode)
            {
                var paymentJson =
                    await paymentResponse.Content.ReadAsStringAsync();

                payments =
                    JsonSerializer.Deserialize<List<PaymentViewModel>>(
                        paymentJson,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new List<PaymentViewModel>();
            }

            ViewBag.Payments = payments;

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(
            string idProduct,
            int qty = 1)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var requestData = new
            {
                IdUser = userId,
                IdProduct = idProduct,
                Qty = qty
            };

            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var json = JsonSerializer.Serialize(requestData);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "api/Cart",
                content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["CartError"] =
                    "Product gagal ditambahkan ke cart.";
            }
            else
            {
                TempData["CartSuccess"] =
                    "Product berhasil ditambahkan ke cart.";
            }

            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public async Task<IActionResult> Update(
            string id,
            int qty)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (qty <= 0)
            {
                qty = 1;
            }

            var requestData = new
            {
                Qty = qty
            };

            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var json = JsonSerializer.Serialize(requestData);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            await client.PutAsync(
                $"api/Cart/{id}",
                content);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            await client.DeleteAsync($"api/Cart/{id}");

            return RedirectToAction("Index");
        }

        // CHECKOUT
        [HttpPost]
        public async Task<IActionResult> Checkout(string idPayment)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (string.IsNullOrEmpty(idPayment))
            {
                TempData["CartError"] =
                    "Silakan pilih metode pembayaran.";

                return RedirectToAction("Index");
            }

            var requestData = new
            {
                IdPayment = idPayment
            };

            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var json = JsonSerializer.Serialize(requestData);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                $"api/Transaction/checkout/{userId}",
                content);

            if (!response.IsSuccessStatusCode)
            {
                var errorJson =
                    await response.Content.ReadAsStringAsync();

                TempData["CartError"] =
                    "Checkout gagal. Pastikan cart kamu tidak kosong.";

                return RedirectToAction("Index");
            }

            TempData["CheckoutSuccess"] =
                "Checkout berhasil! Pesanan kamu sudah dibuat.";

            return RedirectToAction(
                "Index",
                "Transaction");
        }
    }
}