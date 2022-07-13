using ClothesShop.API.Models;

namespace ClothesShop.API.Interfaces
{
    public interface IImageRepository
    {
        // Get all images
        Task<List<Image>> GetAsync();

        // Get image by id
        Task<Image> GetByIdAsync(int id);

        // Post image
        Task<Image> PostAsync(Image image);

        // Put image
        Task<Image> PutAsync(Image image);

        // Delete image
        Task DeleteAsync(int id);
    }
}
