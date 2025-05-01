using KnifeShop.Contracts.Category;
using KnifeShop.DB.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KnifeShop.DB.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(string categoryName, string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name cannot be empty.");

            var maxPosition = await _context.Categories
             .Select(c => (int?)c.PositionId)
             .MaxAsync() ?? 0;

            var category = new Category { Name = categoryName, Image = imagePath, PositionId = maxPosition + 1 };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveCategory(long categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category is null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MoveCategory(long categoryId, bool isMoveUp)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category is null) return false;

            var targetCategoryQuery = _context.Categories
                .Where(c => isMoveUp ? c.PositionId < category.PositionId : c.PositionId > category.PositionId);

            targetCategoryQuery = isMoveUp
                ? targetCategoryQuery.OrderByDescending(c => c.PositionId)
                : targetCategoryQuery.OrderBy(c => c.PositionId);

            var targetCategory = await targetCategoryQuery.FirstOrDefaultAsync();

            if (targetCategory is null) return false;

            var temp = category.PositionId;
            category.PositionId = targetCategory.PositionId;
            targetCategory.PositionId = temp;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category?> GetCategory(long categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            return await _context.Categories
             .Select(c => new CategoryDto
             {
                 Id = c.Id,
                 PositionId = c.PositionId,
                 Name = c.Name,
                 Image = c.Image
             })
             .OrderBy(c => c.PositionId)
             .ToListAsync();
        }
    }
}
