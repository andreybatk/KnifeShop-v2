using KnifeShop.DB.Contracts;
using KnifeShop.DB.Enums;
using KnifeShop.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace KnifeShop.DB.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddFavoriteResult> AddFavoriteKnife(long knifeId, Guid userId)
        {
            var knife = await _context.Knifes.FindAsync(knifeId);
            if (knife is null)
                return AddFavoriteResult.KnifeNotFound;

            var exists = await _context.FavoriteKnifes
                .AnyAsync(fk => fk.KnifeId == knifeId && fk.UserId == userId);

            if (exists)
                return AddFavoriteResult.AlreadyExists;

            var favoriteKnife = new FavoriteKnife
            {
                UserId = userId,
                KnifeId = knifeId,
                AddedAt = DateTime.UtcNow
            };

            await _context.FavoriteKnifes.AddAsync(favoriteKnife);
            await _context.SaveChangesAsync();

            return AddFavoriteResult.Success;
        }

        public async Task<AddFavoriteResult> RemoveFavoriteKnife(long knifeId, Guid userId)
        {
            var favoriteKnife = await _context.FavoriteKnifes
                .FirstOrDefaultAsync(fk => fk.KnifeId == knifeId && fk.UserId == userId);

            if (favoriteKnife is null)
                return AddFavoriteResult.KnifeNotFound;

            _context.FavoriteKnifes.Remove(favoriteKnife);
            await _context.SaveChangesAsync();

            return AddFavoriteResult.Success;
        }

        public async Task<(List<GetKnifesResponse> Items, int TotalCount)> GetFavoriteKnives(Guid userId, int page = 1, int pageSize = 10)
        {
            var query = _context.FavoriteKnifes
                .Where(fk => fk.UserId == userId && fk.Knife != null)
                .OrderByDescending(fk => fk.AddedAt)
                .Include(fk => fk.Knife)
                .Select(fk => fk.Knife!);

            int totalCount = await query.CountAsync();

            var items = await query
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .Select(k => new GetKnifesResponse
                 {
                     Id = k.Id,
                     Title = k.Title,
                     Category = k.Category,
                     Image = k.Image,
                     Price = k.Price,
                     IsOnSale = k.IsOnSale,
                     IsFavorite = k.FavoriteKnifes.Any(f => f.UserId == userId)
                 })
                 .ToListAsync();

            return (items, totalCount);
        }
    }
}
