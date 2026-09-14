using BelanjaYuk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace BelanjaYuk.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient("BelanjaYukAPI");

            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "api/Auth/login",
                content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Email/No HP atau password salah.";
                return View(model);
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<LoginResponse>(
                responseJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result?.User == null)
            {
                ViewBag.Error = "Data user tidak ditemukan.";
                return View(model);
            }

            HttpContext.Session.SetString(
                "UserId",
                result.User.IdUser);

            HttpContext.Session.SetString(
                "UserName",
                result.User.UserName ?? "");

            HttpContext.Session.SetString(
                "UserEmail",
                result.User.Email ?? "");

            return RedirectToAction("Index", "Home");
        }

        // LOGOUT
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        // REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // CEK CONFIRM PASSWORD
            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.Error =
                    "Password dan konfirmasi password tidak sama.";

                return View(model);
            }

            // CEK TANGGAL LAHIR
            DateTime? dateOfBirth = null;

            if (!string.IsNullOrWhiteSpace(model.DOB))
            {
                if (DateTime.TryParseExact(
                    model.DOB,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
                {
                    dateOfBirth = parsedDate;
                }
                else
                {
                    ViewBag.Error =
                        "Format tanggal lahir harus DD/MM/YYYY. Contoh: 12/05/1996.";

                    return View(model);
                }
            }

            // CEK GENDER
            if (string.IsNullOrWhiteSpace(model.IdGender))
            {
                ViewBag.Error =
                    "Silakan pilih gender.";

                return View(model);
            }

            // CEK MODEL
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient(
                "BelanjaYukAPI");

            var requestData = new
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DOB = dateOfBirth,
                IdGender = model.IdGender,
                Password = model.Password
            };

            var json = JsonSerializer.Serialize(requestData);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "api/Auth/register",
                content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "Registrasi gagal. Username, email, atau nomor HP mungkin sudah digunakan.";

                return View(model);
            }

            return RedirectToAction("Login");
        }
    }

    public class LoginResponse
    {
        public string? Message { get; set; }

        public LoginUser? User { get; set; }
    }

    public class LoginUser
    {
        public string IdUser { get; set; } = "";

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}