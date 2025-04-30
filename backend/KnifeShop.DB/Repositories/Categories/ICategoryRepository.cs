using KnifeShop.Contracts.Category;

namespace KnifeShop.DB.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategory(string categoryName, string? imagePath);
        Task<bool> RemoveCategory(long categoryId);
        Task<List<CategoryDto>> GetCategories();
    }
}
