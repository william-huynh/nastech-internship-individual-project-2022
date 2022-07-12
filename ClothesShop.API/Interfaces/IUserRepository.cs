using ClothesShop.API.Models;

namespace ClothesShop.API.Interfaces
{
    public interface IUserRepository
    {
        // Get all users
        Task<List<User>> GetAsync();

        // Get user by id
        Task<User> GetByIdAsync(int id);

        // Post user
        Task<User> PostAsync(User user);

        // Put user
        Task<User> PutAsync(User user);

        // Delete user
        Task DeleteAsync(int id);
    }
}