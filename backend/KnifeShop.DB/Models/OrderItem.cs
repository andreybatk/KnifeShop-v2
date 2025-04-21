namespace KnifeShop.DB.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public long KnifeId { get; set; }
        public Knife? Knife { get; set; }

        public double PriceAtPurchase { get; set; }
    }
}
