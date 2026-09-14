using System.ComponentModel.DataAnnotations;

namespace BelanjaYuk.API.Models
{
    public class MsUser
    {
        [Key]
        public string IdUser { get; set; } = string.Empty;

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateUp { get; set; }
        public string? UserIn { get; set; }
        public string? UserUp { get; set; }
        public bool? IsActive { get; set; }
        public string? IdGender { get; set; }
    }
}