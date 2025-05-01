using System.ComponentModel.DataAnnotations;

namespace KnifeShop.Contracts.Category
{
    public class MoveCategoryRequest
    {
        [Required]
        public bool IsMoveUp { get; set; }
    }
}