namespace BelanjaYuk.Web.Models
{
    public class CategoryViewModel
    {
        public string IdCategory { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public string? CategoryDesc { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateUp { get; set; }
        public string? UserIn { get; set; }
        public string? UserUp { get; set; }
        public bool? IsActive { get; set; }
    }
}