using Microsoft.AspNetCore.Http;

namespace KnifeShop.Contracts.Knife
{
    public class EditKnifeRequest
    {
        public string Title { get; set; } = string.Empty;
        public List<long> CategoryIds { get; set; } = [];
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public List<IFormFile>? Images { get; set; }
        public bool IsOnSale { get; set; }
        public KnifeInfoDto? KnifeInfo { get; set; } = null!;
    }
}
