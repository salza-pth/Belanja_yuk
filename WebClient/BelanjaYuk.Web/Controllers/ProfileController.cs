using BelanjaYuk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BelanjaYuk.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // PROFILE
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
                    $"api/Profile/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error =
                    "Gagal mengambil data profile.";

                return View(
                    new ProfileViewModel());
            }

            var json =
                await response.Content.ReadAsStringAsync();

            var profile =
                JsonSerializer.Deserialize<ProfileViewModel>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })
                ?? new ProfileViewModel();

            return View(profile);
        }

        // EDIT PROFILE
        [HttpGet]
        public async Task<IActionResult> Edit()
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
                    $"api/Profile/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ProfileError"] =
                    "Gagal mengambil data profile.";

                return RedirectToAction("Index");
            }

            var json =
                await response.Content.ReadAsStringAsync();

            var profile =
                JsonSerializer.Deserialize<ProfileViewModel>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })
                ?? new ProfileViewModel();

            var editModel = new ProfileUpdateViewModel
            {
                UserName = profile.UserName,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                DOB = profile.DateOfBirth ?? "",
                IdGender = profile.Gender == "Laki-Laki"
                    ? "GEN001"
                    : profile.Gender == "Perempuan"
                        ? "GEN002"
                        : ""
            };

            return View(editModel);
        }

        // SIMPAN PROFILE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            ProfileUpdateViewModel model)
        {
            var userId =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (string.IsNullOrWhiteSpace(model.UserName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.PhoneNumber) ||
                string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.IdGender))
            {
                ViewBag.Error =
                    "Semua data profile wajib diisi.";

                return View(model);
            }

            DateTime? dob = null;

            if (!string.IsNullOrWhiteSpace(model.DOB))
            {
                if (!DateTime.TryParse(
                    model.DOB,
                    out DateTime parsedDob))
                {
                    ViewBag.Error =
                        "Format tanggal lahir tidak valid.";

                    return View(model);
                }

                dob = parsedDob;
            }

            var requestData = new
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DOB = dob,
                IdGender = model.IdGender
            };

            var client =
                _httpClientFactory.CreateClient("BelanjaYukAPI");

            var json =
                JsonSerializer.Serialize(requestData);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await client.PutAsync(
                    $"api/Profile/{userId}",
                    content);

            if (!response.IsSuccessStatusCode)
            {
                var errorJson =
                    await response.Content.ReadAsStringAsync();

                try
                {
                    var errorObject =
                        JsonSerializer.Deserialize<
                            Dictionary<string, string>>(
                                errorJson);

                    ViewBag.Error =
                        errorObject?["message"]
                        ?? "Profile gagal diperbarui.";
                }
                catch
                {
                    ViewBag.Error =
                        "Profile gagal diperbarui.";
                }

                return View(model);
            }

            // Update session username
            HttpContext.Session.SetString(
                "UserName",
                model.UserName);

            HttpContext.Session.SetString(
                "UserEmail",
                model.Email);

            TempData["ProfileSuccess"] =
                "Profile berhasil diperbarui.";

            return RedirectToAction("Index");
        }
    }
}