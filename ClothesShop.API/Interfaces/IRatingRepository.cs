using ClothesShop.API.Models;

namespace ClothesShop.API.Interfaces
{
    public interface IRatingRepository
    {
        // Get all ratings
        Task<List<Rating>> GetAsync();
        
        // Get rating by id
        Task<Rating> GetByIdAsync(int id);

        // Post rating
        Task<Rating> PostAsync(Rating rating);

        // Put rating
        Task<Rating> PutAsync(Rating rating);

        // Delete rating
        Task DeleteAsync(int id);
    }
}
