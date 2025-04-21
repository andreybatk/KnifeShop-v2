using System.ComponentModel.DataAnnotations;

namespace KnifeShop.API.Contracts.User
{
    public class AddFavoriteKnifeRequest
    {
        [Required]
        public long Id { get; set; }
    }
}
