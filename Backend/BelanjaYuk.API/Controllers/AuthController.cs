using BelanjaYuk.API.Data;
using BelanjaYuk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelanjaYuk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BelanjaYukContext _context;

        public AuthController(BelanjaYukContext context)
        {
            _context = context;
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.MsUser
                .FirstOrDefaultAsync(x =>
                    x.Email == request.EmailOrPhone ||
                    x.PhoneNumber == request.EmailOrPhone);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Email/No HP atau password salah."
                });
            }

            var password = await _context.MsUserPassword
                .FirstOrDefaultAsync(x =>
                    x.IdUser == user.IdUser &&
                    x.IsActive == true);

            if (password == null ||
                password.PasswordHashed != request.Password)
            {
                return Unauthorized(new
                {
                    message = "Email/No HP atau password salah."
                });
            }

            return Ok(new
            {
                message = "Login berhasil.",
                user = new
                {
                    user.IdUser,
                    user.UserName,
                    user.Email,
                    user.PhoneNumber,
                    user.FirstName,
                    user.LastName
                }
            });
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Semua data wajib diisi."
                });
            }

            var existingUser = await _context.MsUser
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email ||
                    x.PhoneNumber == request.PhoneNumber ||
                    x.UserName == request.UserName);

            if (existingUser != null)
            {
                return Conflict(new
                {
                    message = "Username, email, atau nomor HP sudah digunakan."
                });
            }

            var lastUser = await _context.MsUser
                .OrderByDescending(x => x.IdUser)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastUser != null &&
                lastUser.IdUser.StartsWith("USR") &&
                int.TryParse(lastUser.IdUser.Substring(3), out int number))
            {
                nextNumber = number + 1;
            }

            string newUserId = $"USR{nextNumber:D3}";
            string newPasswordId = $"PASS{nextNumber:D3}";

            var newUser = new MsUser
            {
                IdUser = newUserId,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DOB = request.DOB,
                IdGender = request.IdGender,
                DateIn = DateTime.Now,
                IsActive = true
            };

            var newPassword = new MsUserPassword
            {
                IdUserPassword = newPasswordId,
                IdUser = newUserId,
                PasswordHashed = request.Password,
                DateIn = DateTime.Now,
                IsActive = true
            };

            _context.MsUser.Add(newUser);
            _context.MsUserPassword.Add(newPassword);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Registrasi berhasil.",
                user = new
                {
                    newUser.IdUser,
                    newUser.UserName,
                    newUser.Email,
                    newUser.PhoneNumber,
                    newUser.FirstName,
                    newUser.LastName,
                    newUser.DOB,
                    newUser.IdGender
                }
            });
        }
    }
}