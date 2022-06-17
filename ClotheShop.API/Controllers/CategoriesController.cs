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
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            return Ok(await _context.Categories.ToListAsync());
        }

        // GET (single product): api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int? id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<List<Category>>> PostCategory(Category category)
        {
            var isNameAvailable = await _context.Categories.Where(n => n.Name == category.Name).FirstOrDefaultAsync();
            System.Diagnostics.Debug.WriteLine(isNameAvailable);
            if (isNameAvailable != null)
            {
                return Problem("Category name is already existed! Please choose another name");
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(await _context.Categories.ToListAsync());
        }
    }
}
