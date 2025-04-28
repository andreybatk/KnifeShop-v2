using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KnifeShop.Contracts.Knife
{
    public class CreateKnifeRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public List<long> CategoryIds { get; set; } = [];
        [Required]
        public double Price { get; set; }
        public string? Description { get; set; }
        public bool IsOnSale { get; set; }
        public IFormFile? Image { get; set; }
        public List<IFormFile>? Images { get; set; }
        public KnifeInfoDto? KnifeInfo { get; set; } = null!;
    }
}