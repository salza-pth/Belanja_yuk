using System.ComponentModel.DataAnnotations;

namespace BelanjaYuk.API.Models
{
    public class TrBuyerTransaction
    {
        [Key]
        public string IdBuyerTransaction { get; set; } = string.Empty;

        public string? IdUser { get; set; }

        public string? IdPayment { get; set; }

        public decimal? FinalPrice { get; set; }

        public int? Rating { get; set; }

        public string? RatingComment { get; set; }

        public DateTime? DateIn { get; set; }

        public DateTime? DateUp { get; set; }

        public string? UserIn { get; set; }

        public string? UserUp { get; set; }

        public bool? IsActive { get; set; }
    }
}