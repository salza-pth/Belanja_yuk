using System.ComponentModel.DataAnnotations;

namespace BelanjaYuk.API.Models
{
    public class TrBuyerCart
    {
        [Key]
        public string IdBuyerCart { get; set; } = string.Empty;

        public string? IdUser { get; set; }

        public string? IdProduct { get; set; }

        public int? Qty { get; set; }

        public DateTime? DateIn { get; set; }

        public DateTime? DateUp { get; set; }

        public string? UserIn { get; set; }

        public string? UserUp { get; set; }

        public bool? IsActive { get; set; }
    }
}