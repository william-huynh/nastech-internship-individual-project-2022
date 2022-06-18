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
        private readonly ClothesDbContext _context;

        public ClothesController(ClothesDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Category>> GetClothes()
        //{
        //    IList<Category> categories = null;

        //    using (var context = _context)
        //    {
        //        categories = context.Categories.Select(s => new Category()
        //        {
        //            Id = s.Id,
        //            Name = s.Name,
        //            Description = s.Description,
        //        })
        //    }
        //}

        //// GET: api/Clothes
        //[HttpGet]
        //public async  Task<ActionResult<IEnumerable<Clothes>>> GetClothes()
        //{
        //    if (_context.Clothes == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _context.Clothes.ToListAsync();
        //}

        //[HttpPost]
        //public IHttpActionResult PostClothes(Clothes obj)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return (IHttpActionResult)BadRequest("Invalid data");
        //    }
        //    using (var context = _context)
        //    {
        //        context.Clothes.Add(new Clothes()
        //        {
        //            ID = obj.ID,
        //            Name = obj.Name,
        //            Description = obj.Description,
        //            CategoryID = obj.CategoryID,
        //            Price = obj.Price,
        //            Stock = obj.Stock,
        //            AddedDate = DateTime.Now,
        //            Category = obj.Category,
        //        });
        //        context.SaveChanges();
        //    }
        //    return (IHttpActionResult)Ok();
        //}

        // POST: api/Clothes
        [HttpPost]
        public ActionResult<Clothes> PostClothes(Clothes obj)
        {
            if (_context.Clothes == null)
            {
                return Problem("_context.Clothes is null");
            }
            _context.Clothes.Add(obj);
            _context.SaveChangesAsync();

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
