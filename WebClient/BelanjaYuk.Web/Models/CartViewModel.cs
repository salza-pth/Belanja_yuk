namespace BelanjaYuk.Web.Models
{
    public class CartViewModel
    {
        public string IdBuyerCart { get; set; } = "";

        public string? IdUser { get; set; }

        public string? IdProduct { get; set; }

        public int? Qty { get; set; }

        public DateTime? DateIn { get; set; }

        public DateTime? DateUp { get; set; }

        public string? UserIn { get; set; }

        public string? UserUp { get; set; }

        public bool? IsActive { get; set; }

        // Data tambahan untuk tampilan
        public string ProductName { get; set; } = "";

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }
    }
}