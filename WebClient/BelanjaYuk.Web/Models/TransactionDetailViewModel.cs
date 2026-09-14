namespace BelanjaYuk.Web.Models
{
    public class TransactionDetailViewModel
    {
        public string IdProduct { get; set; } = "";
        public string ProductName { get; set; } = "";
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Subtotal { get; set; }
    }
}