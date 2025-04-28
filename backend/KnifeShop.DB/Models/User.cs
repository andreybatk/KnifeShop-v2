using Microsoft.AspNetCore.Identity;

namespace KnifeShop.DB.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<FavoriteKnife> FavoriteKnifes { get; set; } = [];
        public List<Order> Orders { get; set; } = [];
    }
}