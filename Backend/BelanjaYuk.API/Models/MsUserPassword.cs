using System.ComponentModel.DataAnnotations;

namespace BelanjaYuk.API.Models
{
    public class MsUserPassword
    {
        [Key]
        public string IdUserPassword { get; set; } = string.Empty;

        public string IdUser { get; set; } = string.Empty;

        public string? PasswordHashed { get; set; }

        public DateTime? DateIn { get; set; }
        public DateTime? DateUp { get; set; }
        public string? UserIn { get; set; }
        public string? UserUp { get; set; }
        public bool? IsActive { get; set; }
    }
}