using AutoMapper;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ClothesDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ClothesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriesListDto> GetAsyncList(int? page, int? pageSize)
        {
            var categoryList = _context.Categories.Where(category => category.IsDeleted.Equals(false));
            var pageRecords = pageSize ?? 10;
            var pageIndex = page ?? 1;
            int totalPage = categoryList.Count();
            var startPage = (pageIndex - 1) * pageRecords;
            if (totalPage > pageRecords)
                categoryList = categoryList.Skip(startPage).Take(pageRecords);
            var listDetailCategory = await categoryList.ToListAsync();
            var listDetailCategoryDto = _mapper.Map<List<CategoryDto>>(listDetailCategory);
            var categoryDto = _mapper.Map<CategoriesListDto>(listDetailCategoryDto);
            categoryDto.TotalItem = totalPage;
            categoryDto.CurrentPage = pageIndex;
            categoryDto.PageSize = pageRecords;
            return categoryDto;
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
