using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data.Repositories
{
    public class ClothesRepository : IClothesRepository
    {
        private readonly ClothesDbContext _context;

        public ClothesRepository(ClothesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Clothes>> GetAsync()
        {
            return await _context.Clothes
                .Where(clothes => clothes.IsDeleted.Equals(false)).ToListAsync();
        }

        public async Task<List<Clothes>> GetByCategoryId(int id)
        {
            return await _context.Clothes.Where(clothes => clothes.CategoryId.Equals(id) && clothes.IsDeleted.Equals(false)).ToListAsync();
        }

        public async Task<Clothes> GetByIdAsync(int id)
        {
            return await _context.Clothes
                .AsNoTracking()
                .FirstOrDefaultAsync(clothes => clothes.ID.Equals(id) && clothes.IsDeleted.Equals(false));
        }

        public async Task<Clothes> PostAsync(Clothes clothes)
        {
            _context.Clothes.Add(clothes);
            await _context.SaveChangesAsync();
            return clothes;
        }

        public async Task<Clothes> PutAsync(Clothes clothes)
        {
            _context.Clothes.Update(clothes);
            await _context.SaveChangesAsync();
            return clothes;
        }

        public async Task DeleteAsync(int id)
        {
            var clothes = await _context.Clothes.FirstOrDefaultAsync(clothes => clothes.ID.Equals(id));
            clothes.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
