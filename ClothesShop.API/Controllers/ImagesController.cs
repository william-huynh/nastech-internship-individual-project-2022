using ClotheShop.API.Data;
using ClotheShop.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ClothesDbContext _context;

        public ImagesController(ClothesDbContext context)
        {
            _context = context;
        }

        // GET (all): api/Images
        [HttpGet]
        public IActionResult GetAllImages()
        {
            try
            {
                var allImages = _context.Images.Select(ai => new ImagesModel
                {
                    Id = ai.Id,
                    URL = ai.URL,
                    IsDeleted = ai.IsDeleted,
                    ClothesId = ai.ClothesID,
                }).ToList();
                return Ok(allImages.Where(ai => ai.IsDeleted == false));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET (single): api/Images/{id}
        [HttpGet("{id}")]
        public IActionResult GetImage(int? id)
        {
            try
            {
                var checkedImage = _context.Images.Find(id);
                if (checkedImage == null || checkedImage.IsDeleted == true)
                {
                    return NotFound();
                }
                var singleImage = new ImagesModel
                {
                    Id = checkedImage.Id,
                    URL = checkedImage.URL,
                    IsDeleted = checkedImage.IsDeleted,
                };
                return Ok(singleImage);
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Images
        [HttpPost]
        public IActionResult PostImage(ImagesModel imagesModel)
        {
            try
            {
                // Find reference clothes with clothesId => return clothes or null
                var checkedClothes = _context.Clothes.Find(imagesModel.ClothesId);

                // Find image with similar URL & active => return image or null
                var checkedImage = _context.Images.FirstOrDefault(
                    i => i.URL == imagesModel.URL && i.IsDeleted == false);

                // Check if reference clothes available or not
                if (checkedClothes == null || checkedClothes.IsDeleted == true)
                {
                    return BadRequest("Clothes is unavailable!");
                }

                // If there is image with similar URL
                if (checkedImage != null)
                {
                    // If that image has similar clothesId as well
                    if (checkedImage.ClothesID == imagesModel.ClothesId)
                    {
                        return BadRequest("Similar image existed!");
                    }
                }

                // There is
                //  ->  Image with similar URL & different clothesId
                //  ->  Image with different URL & similar clothesId
                //  ->  Image with different URL & different clothesId
                var newImage = new Image
                {
                    URL = imagesModel.URL,
                    ClothesID = imagesModel.ClothesId,
                };
                _context.Images.Add(newImage);
                _context.SaveChanges();
                return Ok(new ImagesModel
                {
                    Id = newImage.Id,
                    ClothesId = newImage.ClothesID,
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // Images POCO model (for testing)
        public class ImagesModel
        {
            public int Id { get; set; }
            public string URL { get; set; }
            public bool IsDeleted { get; set; } = false;
            public int ClothesId { get; set; }
        }
    }
}
