using System.ComponentModel.DataAnnotations;

namespace KnifeShop.API.Contracts.Knife
{
    public class CreateKnifeRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        public string? Description { get; set; }
        public bool IsOnSale { get; set; }
        public IFormFile? Image { get; set; }
        public List<IFormFile>? Images { get; set; }
        public KnifeInfoRequest? KnifeInfo { get; set; } = null!;
    }
}