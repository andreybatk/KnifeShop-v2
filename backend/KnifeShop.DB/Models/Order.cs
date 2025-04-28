namespace KnifeShop.DB.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; }
        public bool IsShipped { get; set; }
        public bool IsDelivered { get; set; }

        public List<OrderItem> Items { get; set; } = [];
    }
}
