namespace KnifeShop.API.Contracts.Knife
{
    public class EditKnifeRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public List<IFormFile>? Images { get; set; }
        public bool IsOnSale { get; set; }
        public KnifeInfoRequest? KnifeInfo { get; set; } = null!;
    }
}
