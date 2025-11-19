namespace Cabarles_IPT.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        public decimal Total => Quantity * PricePerItem;
    }
}
