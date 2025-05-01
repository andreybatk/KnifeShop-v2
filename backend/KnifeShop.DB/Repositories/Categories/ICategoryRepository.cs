using KnifeShop.Contracts.Category;
using KnifeShop.DB.Models;

namespace KnifeShop.DB.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategory(string categoryName, string? imagePath);
        Task<bool> RemoveCategory(long categoryId);
        Task<bool> MoveCategory(long categoryId, bool isMoveUp);
        Task<Category?> GetCategory(long categoryId);
        Task<List<CategoryDto>> GetCategories();
    }
}
