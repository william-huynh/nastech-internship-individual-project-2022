using ClotheShop.API.Data;
using ClotheShop.API.Models;
using ClothesShop.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClothesShop.API.Controllers.ImagesController;

namespace ClothesShop.API.Controllers
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

        // GET (all): api/Clothes
        [HttpGet]
        public IActionResult GetAllClothes()
        {
            try
            {
                var allClothes = _context.Clothes.Include(clothes => clothes.Images).Select(clothes => new ClothesModel
                {
                    Id = clothes.ID,
                    Name = clothes.Name,
                    Description = clothes.Description,
                    Price = clothes.Price,
                    Stock = clothes.Stock,
                    AddedDate = clothes.AddedDate,
                    CategoryId = clothes.CategoryId, 
                    IsDeleted = clothes.IsDeleted,
                    Images = clothes.Images.Select(images => new ImagesModel
                    {
                        Id = images.Id,
                        URL = images.URL,
                        ClothesId = images.ClothesID,
                    }).ToList()
                }).ToList();
                return Ok(allClothes);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Clothes POCO model (for testing)
        public class ClothesModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int Stock { get; set; }
            public DateTime AddedDate { get; set; }
            public bool IsDeleted { get; set; }
            public int CategoryId { get; set; }
            public List<ImagesModel> Images { get; set; } = new List<ImagesModel>();

        }
    }
}
