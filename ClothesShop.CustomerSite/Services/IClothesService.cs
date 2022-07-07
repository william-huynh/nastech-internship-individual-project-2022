using ClothesShop.SharedVMs;
using Refit;

namespace ClothesShop.CustomerSite.Services
{
    public interface IClothesService
    {
        // Get all clothes
        [Get("/Clothes")]
        Task<List<ClothesDto>> GetAllClothes();

        // Get clothes by Id
        [Get("/Clothes/{id}")]
        Task<ClothesDto> GetClothes(int id);

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
