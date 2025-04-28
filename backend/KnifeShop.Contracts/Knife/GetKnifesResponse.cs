using KnifeShop.Contracts.Category;

namespace KnifeShop.Contracts.Knife
{
    public class GetKnifesResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<CategoryDto> Categories { get; set; } = [];
        public string? Image { get; set; }
        public double Price { get; set; }
        public bool IsOnSale { get; set; }

        public bool IsFavorite { get; set; } = false;
    }
}