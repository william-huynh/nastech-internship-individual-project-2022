using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ClothesDbContext _context;

        public CategoryRepository(ClothesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAsync()
        {
            return await _context.Categories.Where(category => category.IsDeleted.Equals(false)).ToListAsync();
        }

        public async Task<List<Category>> Get5Async()
        {
            return await _context.Categories.Where(category => category.IsDeleted.Equals(false)).Take(5).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id.Equals(id) && category.IsDeleted.Equals(false));
        }

        public async Task<Category> PostAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> PutAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(category => category.Id.Equals(id));
            category.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
