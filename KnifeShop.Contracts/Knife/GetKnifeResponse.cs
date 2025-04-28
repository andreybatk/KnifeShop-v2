using KnifeShop.Contracts.Category;

namespace KnifeShop.Contracts.Knife
{
    public class GetKnifeResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<CategoryDto> Categories { get; set; } = [];
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string>? Images { get; set; }
        public double Price { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime CreatedAt { get; set; }
        public KnifeInfoDto? KnifeInfo { get; set; }

        public bool IsFavorite { get; set; } = false;
    }
}
