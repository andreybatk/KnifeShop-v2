namespace KnifeShop.DB.Models
{
    public class FavoriteKnife
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public long KnifeId { get; set; }
        public Knife? Knife { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}