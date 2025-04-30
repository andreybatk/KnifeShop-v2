using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KnifeShop.Contracts.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
