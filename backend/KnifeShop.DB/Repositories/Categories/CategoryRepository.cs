using KnifeShop.Contracts.Category;
using KnifeShop.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace KnifeShop.DB.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name cannot be empty.");

            var category = new Category { Name = categoryName };
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

        public async Task<List<CategoryDto>> GetCategories()
        {
            return await _context.Categories
             .Select(c => new CategoryDto
             {
                 Id = c.Id,
                 Name = c.Name
             })
             .ToListAsync();
        }
    }
}
