using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoriesListDto> GetAsyncList(int? page, int? pageSize);
        
        // Get all categories
        Task<List<Category>> GetAsync();

        // Get 5 categories
        Task<List<Category>> Get5Async();

        // Get category by id
        Task<Category> GetByIdAsync(int id);

        // Post category
        Task<Category> PostAsync(Category category);

        // Put category
        Task<Category> PutAsync(Category category);

        // Delete category
        Task DeleteAsync(int id);
    }
}
