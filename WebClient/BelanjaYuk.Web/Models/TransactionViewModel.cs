namespace BelanjaYuk.Web.Models
{
    public class TransactionViewModel
    {
        public string IdBuyerTransaction { get; set; } = "";

        public string? IdUser { get; set; }

        public string? IdPayment { get; set; }

        public string PaymentName { get; set; } = "";

        public decimal? FinalPrice { get; set; }

        public int? Rating { get; set; }

        public string? RatingComment { get; set; }

        public DateTime? DateIn { get; set; }

        public DateTime? DateUp { get; set; }

        public string? UserIn { get; set; }

        public string? UserUp { get; set; }

        public bool? IsActive { get; set; }

        public List<TransactionDetailViewModel> Details { get; set; }
            = new List<TransactionDetailViewModel>();
    }
}