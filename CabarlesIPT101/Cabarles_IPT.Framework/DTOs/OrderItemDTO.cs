namespace Cabarles_IPT.Framework.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
    }
}
