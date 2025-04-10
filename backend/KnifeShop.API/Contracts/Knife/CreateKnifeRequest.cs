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
        public double? OverallLength { get; set; }
        public double? BladeLength { get; set; }
        public double? ButtThickness { get; set; }
        public double? Weight { get; set; }
        public string? HandleMaterial { get; set; }
        public string? Country { get; set; }
        public string? Manufacturer { get; set; }
        public string? SteelGrade { get; set; }
    }
}