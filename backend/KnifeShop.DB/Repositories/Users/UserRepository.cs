using KnifeShop.DB.Enums;
using KnifeShop.DB.Models;
using KnifeShop.DB.Repositories.Users;
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

        public async Task<List<Knife>> GetFavoriteKnives(Guid userId)
        {
            return await _context.FavoriteKnifes
                .Where(fk => fk.UserId == userId)
                .Include(fk => fk.Knife)
                .Select(fk => fk.Knife!)
                .ToListAsync();
        }
    }
}
