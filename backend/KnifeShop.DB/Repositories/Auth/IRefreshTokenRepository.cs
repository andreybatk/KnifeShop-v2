using KnifeShop.DB.Models;

namespace KnifeShop.DB.Repositories.Token
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByToken(string token);
        Task Create(RefreshToken refreshToken);
        Task Delete(Guid id);
        Task DeleteAll(Guid userId);
    }
}