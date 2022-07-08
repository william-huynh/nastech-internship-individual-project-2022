﻿using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ClothesDbContext _context;

        public RatingRepository(ClothesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rating>> GetAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _context.Ratings.AsNoTracking().FirstOrDefaultAsync(rating => rating.Id.Equals(id));
        }

        public async Task<Rating> PostAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> PutAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(rating => rating.Id.Equals(id));
            rating.IsDelete = true;
            await _context.SaveChangesAsync();
        }
    }
}
