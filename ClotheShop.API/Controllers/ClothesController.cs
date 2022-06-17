using ClotheShop.API.Data;
using ClotheShop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClotheShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly ClotheDbContext _context;

        public ClothesController(ClotheDbContext context)
        {
            _context = context;
        }

        // GET: api/Clothes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clothe>>> GetClothes()
        {
            if (_context.Clothes == null)
            {
                return NotFound();
            }
            return await _context.Clothes.ToListAsync();
        }

        // POST: api/Clothes
        [HttpPost]
        public async Task<ActionResult<Clothe>> PostClothes(Clothe obj)
        {
            if (_context.Clothes == null)
            {
                return Problem("_context.Clothes is null");
            }
            _context.Clothes.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //// POST: api/Categories
        //[HttpPost]
        //public async Task<ActionResult<Category>> PostCategories(Category obj)
        //{
        //    if (_context.Categories == null)
        //    {
        //        return Problem("_context.Categories is null");
        //    }
        //    _context.Categories.Add(obj);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
