using BelanjaYuk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BelanjaYuk.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var response = await client.GetAsync("api/Categories/active");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Gagal mengambil data category dari API.";
                return View(new List<CategoryViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();

            var categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View(categories ?? new List<CategoryViewModel>());
        }
    }
}