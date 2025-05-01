namespace KnifeShop.DB.Models
{
    public class Category
    {
        public long Id { get; set; }
        public int PositionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }
        public List<KnifeCategory> KnifeCategories { get; set; } = [];
    }
}
