namespace BelanjaYuk.Web.Models
{
    public class ProductViewModel
    {
        public string IdProduct { get; set; } = "";
        public string IdUserSeller { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string ProductDesc { get; set; } = "";
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string IdCategory { get; set; } = "";
        public DateTime? DateIn { get; set; }
        public DateTime? DateUp { get; set; }
        public string? UserIn { get; set; }
        public string? UserUp { get; set; }
        public bool? IsActive { get; set; }
    }
}