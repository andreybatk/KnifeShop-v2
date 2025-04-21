using Microsoft.AspNetCore.Identity;

namespace KnifeShop.DB.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<FavoriteKnife> FavoriteKnifes { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}