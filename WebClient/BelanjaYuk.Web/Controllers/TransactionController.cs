using BelanjaYuk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BelanjaYuk.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionController(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client =
                _httpClientFactory.CreateClient("BelanjaYukAPI");

            var response =
                await client.GetAsync(
                    $"api/Transaction/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "Gagal mengambil riwayat transaksi.";

                return View(
                    new List<TransactionViewModel>());
            }

            var json =
                await response.Content.ReadAsStringAsync();

            var transactions =
                JsonSerializer.Deserialize<
                    List<TransactionViewModel>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        })
                ?? new List<TransactionViewModel>();

            return View(transactions);
        }
    }
}