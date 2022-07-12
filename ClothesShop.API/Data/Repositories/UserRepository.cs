using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClothesDbContext _context;

        public UserRepository(ClothesDbContext context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetAsync()
        {
            return await _context.Users.Where(user => user.IsDeleted.Equals(false)).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id.Equals(id) && user.IsDeleted.Equals(false));
        }

        public async Task<User> PostAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> PutAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}