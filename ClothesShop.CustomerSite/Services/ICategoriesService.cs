using ClothesShop.SharedVMs;
using Refit;

namespace ClothesShop.CustomerSite.Services
{
    public interface ICategoriesService
    {
        // Get all categories
        [Get("/Categories")]
        Task<List<CategoryDto>> GetCategories();

        // Get 3 categories
        [Get("/Categories/Quantity")]
        Task<List<CategoryDto>> Get5Categories();

        // Get category by Id
        [Get("/Categories/{id}")]
        Task<CategoryDto> GetCategory(int id);

        // Add category
        [Post("/Categories")]
        Task CreateCategory([Body] CategoryDto category);

        // Update category
        [Put("/Categories/{id}")]
        Task UpdateCategory(int id, [Body] CategoryDto category);

        // Delete category
        [Delete("/Categories/{id}")]
        Task DeleteCategory(int id);
    }
}
