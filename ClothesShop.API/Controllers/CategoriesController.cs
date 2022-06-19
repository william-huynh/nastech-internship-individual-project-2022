using ClotheShop.API.Data;
using ClotheShop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClotheShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ClothesDbContext _context;

        public CategoriesController(ClothesDbContext context)
        {
            _context = context;
        }

        // GET (all): api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var allCategories = await _context.Categories.Select(c => new CategoriesModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsDeleted = c.IsDeleted,
                }).ToListAsync();
                return Ok(allCategories.Where(ac => ac.IsDeleted == false));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET (single): api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int? id)
        {
            try
            {
                var checkedCategory = await _context.Categories.FindAsync(id);
                if (checkedCategory == null || checkedCategory.IsDeleted == true)
                {
                    return NotFound();
                }
                var singleCategory = new CategoriesModel
                {
                    Id = checkedCategory.Id,
                    Name = checkedCategory.Name,
                    Description = checkedCategory.Description,
                    IsDeleted = checkedCategory.IsDeleted,
                };
                return Ok(singleCategory);
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoriesModel categoryModel)
        {
            try
            {
                var newCategory = new Category
                {
                    Name = categoryModel.Name,
                    Description = categoryModel.Description,
                };
                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return Ok(newCategory);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int? id, CategoriesModel categoryModel)
        {
            try
            {
                var updatedCategory = await _context.Categories.FindAsync(id);
                if (updatedCategory == null)
                {
                    return NotFound();
                }
                updatedCategory.Name = categoryModel.Name;
                updatedCategory.Description = categoryModel.Description;
                await _context.SaveChangesAsync();
                return Ok(updatedCategory);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            try
            {
                var deletedCategory = await _context.Categories.FindAsync(id);
                if (deletedCategory == null)
                {
                    return NotFound();
                }
                deletedCategory.IsDeleted = true;
                await _context.SaveChangesAsync();
                return Ok(deletedCategory);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Categories POCO model (for testing)
        public class CategoriesModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsDeleted { get; set; } = false;
        }
    }
}
