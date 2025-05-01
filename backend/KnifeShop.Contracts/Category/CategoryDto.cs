namespace KnifeShop.Contracts.Category
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public int PositionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
    }
}