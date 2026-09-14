using BelanjaYuk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BelanjaYuk.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string? category)
        {
            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            // GET PRODUCTS
            var productUrl = "api/Products";

            if (!string.IsNullOrEmpty(category))
            {
                productUrl += $"?category={Uri.EscapeDataString(category)}";
            }

            var productResponse = await client.GetAsync(productUrl);

            if (!productResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Gagal mengambil data product dari API.";
                return View(new List<ProductViewModel>());
            }

            var productJson = await productResponse.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<ProductViewModel>>(
                productJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // GET CATEGORIES
            var categoryResponse = await client.GetAsync("api/Categories/active");

            var categories = new List<CategoryViewModel>();

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();

                categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(
                    categoryJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<CategoryViewModel>();
            }

            ViewBag.Categories = categories;
            ViewBag.Category = category;

            return View(products ?? new List<ProductViewModel>());
        }

        public async Task<IActionResult> Details(string id)
        {
            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var response = await client.GetAsync($"api/Products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();

            var product = JsonSerializer.Deserialize<ProductViewModel>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}