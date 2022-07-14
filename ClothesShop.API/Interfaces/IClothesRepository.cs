using ClothesShop.API.Models;

namespace ClothesShop.API.Interfaces
{
    public interface IClothesRepository
    {
        // Get all clothes
        Task<List<Clothes>> GetAsync();

        // Get all clothes with same category id
        Task<List<Clothes>> GetByCategoryId(int id);

        // Get clothes by id
        Task<Clothes> GetByIdAsync(int id);

        // Get clothes added date by id
        Task<DateTime> GetAddedDateByIdAsync(int id);

        // Post clothes
        Task<Clothes> PostAsync(Clothes clothes);

        // Put clothes
        Task<Clothes> PutAsync(Clothes clothes);

        // Delete clothes
        Task DeleteAsync(int id);
    }
}
