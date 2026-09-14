using BelanjaYuk.API.Data;
using BelanjaYuk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public ProfileController(BelanjaYukContext context)
        {
            _context = context;
        }

        // GET: api/Profile/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            var user = await _context.MsUser
                .FirstOrDefaultAsync(x =>
                    x.IdUser == userId &&
                    x.IsActive == true);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User tidak ditemukan."
                });
            }

            var gender = await _context.LtGender
                .FirstOrDefaultAsync(x =>
                    x.IdGender == user.IdGender &&
                    x.IsActive == true);

            var profile = new ProfileViewModel
            {
                IdUser = user.IdUser,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                DateOfBirth = user.DOB?.ToString("yyyy-MM-dd"),
                Gender = gender?.GenderName
            };

            return Ok(profile);
        }

        // PUT: api/Profile/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(
            string userId,
            [FromBody] ProfileUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new
                {
                    message = "User tidak valid."
                });
            }

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.IdGender))
            {
                return BadRequest(new
                {
                    message = "Semua data profile wajib diisi."
                });
            }

            var user = await _context.MsUser
                .FirstOrDefaultAsync(x =>
                    x.IdUser == userId &&
                    x.IsActive == true);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User tidak ditemukan."
                });
            }

            // Cek username digunakan user lain
            var usernameExists = await _context.MsUser
                .AnyAsync(x =>
                    x.UserName == request.UserName &&
                    x.IdUser != userId &&
                    x.IsActive == true);

            if (usernameExists)
            {
                return BadRequest(new
                {
                    message = "Username sudah digunakan."
                });
            }

            // Cek email digunakan user lain
            var emailExists = await _context.MsUser
                .AnyAsync(x =>
                    x.Email == request.Email &&
                    x.IdUser != userId &&
                    x.IsActive == true);

            if (emailExists)
            {
                return BadRequest(new
                {
                    message = "Email sudah digunakan."
                });
            }

            // Cek nomor telepon digunakan user lain
            var phoneExists = await _context.MsUser
                .AnyAsync(x =>
                    x.PhoneNumber == request.PhoneNumber &&
                    x.IdUser != userId &&
                    x.IsActive == true);

            if (phoneExists)
            {
                return BadRequest(new
                {
                    message = "Nomor telepon sudah digunakan."
                });
            }

            // Cek gender
            var genderExists = await _context.LtGender
                .AnyAsync(x =>
                    x.IdGender == request.IdGender &&
                    x.IsActive == true);

            if (!genderExists)
            {
                return BadRequest(new
                {
                    message = "Gender tidak valid."
                });
            }

            // Update data
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.DOB = request.DOB;
            user.IdGender = request.IdGender;

            user.DateUp = DateTime.Now;
            user.UserUp = userId;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Profile berhasil diperbarui.",
                profile = new
                {
                    user.IdUser,
                    user.UserName,
                    user.Email,
                    user.PhoneNumber,
                    user.FirstName,
                    user.LastName,
                    user.DOB,
                    user.IdGender
                }
            });
        }
    }
}