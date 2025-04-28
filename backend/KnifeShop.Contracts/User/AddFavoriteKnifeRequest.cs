using System.ComponentModel.DataAnnotations;

namespace KnifeShop.Contracts.User
{
    public class AddFavoriteKnifeRequest
    {
        [Required]
        public long Id { get; set; }
    }
}
