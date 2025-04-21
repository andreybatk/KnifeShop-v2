using KnifeShop.DB.Enums;
using KnifeShop.DB.Models;

namespace KnifeShop.DB.Repositories.Users
{
    public interface IUserRepository
    {
        Task<AddFavoriteResult> AddFavoriteKnife(long knifeId, Guid userId);
        Task<AddFavoriteResult> RemoveFavoriteKnife(long knifeId, Guid userId);
        Task<List<Knife>> GetFavoriteKnives(Guid userId);
    }
}
