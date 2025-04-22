using KnifeShop.DB.Contracts;
using KnifeShop.DB.Enums;
using KnifeShop.DB.Models;

namespace KnifeShop.DB.Repositories.Users
{
    public interface IUserRepository
    {
        Task<AddFavoriteResult> AddFavoriteKnife(long knifeId, Guid userId);
        Task<AddFavoriteResult> RemoveFavoriteKnife(long knifeId, Guid userId);
        Task<(List<GetKnifesResponse> Items, int TotalCount)> GetFavoriteKnives(Guid userId, int page, int pageSize);
    }
}
