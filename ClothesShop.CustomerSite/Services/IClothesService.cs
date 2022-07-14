using ClothesShop.SharedVMs;
using Refit;

namespace ClothesShop.CustomerSite.Services
{
    public interface IClothesService
    {
        // Get all clothes
        [Get("/Clothes")]
        Task<List<ClothesDto>> GetAllClothes();

        // Get clothes by category Id
        [Get("/Clothes/Categories/{id}")]
        Task<List<ClothesDto>> GetByCategoryId(int id);

        // Get 5 clothes
        [Get("/Clothes/Quantity")]
        Task<List<ClothesDto>> Get5Clothes();

        // Get clothes by Id
        [Get("/Clothes/{id}")]
        Task<List<ClothesDto>> GetClothes(int id);

        // Add clothes
        [Post("/Clothes")]
        Task CreateClothes([Body] ClothesDto category);

        // Update clothes
        [Put("/Clothes/{id}")]
        Task UpdateClothes(int id, [Body] ClothesDto category);

        // Delete clothes
        [Delete("/Clothes/{id}")]
        Task DeleteClothes(int id);
    }
}
