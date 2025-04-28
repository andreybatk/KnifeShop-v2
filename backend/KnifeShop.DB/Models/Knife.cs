namespace KnifeShop.DB.Models
{
    public class Knife
    {
        public Knife(string title, string? description, string? image, List<string>? images, double price, bool isOnSale)
        {
            Title = title;
            Description = description;
            Image = image;
            Images = images;
            Price = price;
            IsOnSale = isOnSale;
        }

        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<KnifeCategory> KnifeCategories { get; set; } = [];
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string>? Images { get; set; }
        public double Price { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // UtcNow for PostgreSQL
        public KnifeInfo? KnifeInfo { get; set; }
        public List<FavoriteKnife> FavoriteKnifes { get; set; } = [];
    }
}
