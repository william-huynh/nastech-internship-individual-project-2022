using ClothesShop.API.Models;
using ClothesShop.SharedVMs;

namespace ClothesShop.API.Interfaces
{
    public interface IClothesRepository
    {
        Task<ClothesListDto> GetAsyncList(int? page, int? pageSize);

        // Get all clothes
        Task<List<Clothes>> GetAsync();

        // Get all clothes (include deleted)
        Task<List<Clothes>> GetAllAsync();

        // Get all clothes with same category id
        Task<List<Clothes>> GetByCategoryId(int id);

        // Get 5 clothes
        Task<List<Clothes>> Get5ClothesAsync();

        // Get clothes by id
        Task<List<Clothes>> GetByIdAsync(int id);

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
