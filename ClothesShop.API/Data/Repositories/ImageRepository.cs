using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ClothesDbContext _context;

        public ImageRepository(ClothesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Image>> GetAsync()
        {
            return await _context.Images.Where(image => image.IsDeleted.Equals(false)).ToListAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _context.Images.AsNoTracking().FirstOrDefaultAsync(image => image.Id.Equals(id) && image.IsDeleted.Equals(false));
        }

        public async Task<Image> PostAsync(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image> PutAsync(Image image)
        {
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(image => image.Id.Equals(id));
            image.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
