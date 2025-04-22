namespace KnifeShop.DB.Contracts
{
    public class GetKnifesResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Image { get; set; }
        public double Price { get; set; }
        public bool IsOnSale { get; set; }

        public bool IsFavorite { get; set; } = false;
    }
}