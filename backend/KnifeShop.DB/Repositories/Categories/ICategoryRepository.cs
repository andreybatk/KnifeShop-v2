using KnifeShop.Contracts.Category;

namespace KnifeShop.DB.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategory(string categoryName);
        Task<bool> RemoveCategory(long categoryId);
        Task<List<CategoryDto>> GetCategories();
    }
}
