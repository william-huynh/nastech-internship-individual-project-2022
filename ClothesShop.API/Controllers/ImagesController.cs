using ClothesShop.API.Data;
using ClothesShop.API.Models;
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
                var allImages = _context.Images.Select(images => new ImagesModel
                {
                    Id = images.Id,
                    URL = images.URL,
                    ClothesId = images.ClothesID,
                    IsDeleted = images.IsDeleted,
                }).ToList();
                return Ok(allImages.Where(images => images.IsDeleted == false));
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET (single): api/Images/{id}
        [HttpGet("GetById/{id}")]
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
                    ClothesId = checkedImage.ClothesID,
                    IsDeleted = checkedImage.IsDeleted,
                };
                return Ok(singleImage);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetByClothesId/{clothesId}")]
        public List<ImagesModel> GetImagesOfClothes(int clothesId)
        {
            var imagesOfClothes = _context.Images.Select(i => new ImagesModel
            {
                Id = i.Id,
                ClothesId = i.ClothesID,
            }).Where(i => i.ClothesId == clothesId).ToList();
            return imagesOfClothes;
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
                    similarURLImage => similarURLImage.URL == imagesModel.URL && similarURLImage.IsDeleted == false);

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
                    URL = newImage.URL,
                    ClothesId = newImage.ClothesID,
                    IsDeleted = newImage.IsDeleted,
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Images/{id}
        [HttpPut("{id}")]
        public IActionResult PutImage(int id, ImagesModel imagesModel)
        {
            try
            {
                var updatedImage = _context.Images.Find(id);
                var checkedImage = _context.Images.FirstOrDefault(
                    similarURLImage => similarURLImage.URL == imagesModel.URL && similarURLImage.IsDeleted == false);
                if (updatedImage == null || updatedImage.IsDeleted == true)
                {
                    return NotFound();
                }
                if (checkedImage != null && checkedImage.ClothesID == imagesModel.ClothesId)
                {
                    return BadRequest("Similar image existed!");
                }
                updatedImage.URL = imagesModel.URL;
                _context.SaveChanges();
                return Ok(new ImagesModel
                {
                    Id = updatedImage.Id,
                    URL = updatedImage.URL,
                    ClothesId = updatedImage.ClothesID,
                    IsDeleted = updatedImage.IsDeleted,
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Images/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            try
            {
                var deletedImage = _context.Images.Find(id);
                if (deletedImage == null || deletedImage.IsDeleted == true)
                {
                    return NotFound();
                }
                deletedImage.IsDeleted = true;
                _context.SaveChanges();
                return Ok(new ImagesModel
                {
                    Id = deletedImage.Id,
                    URL = deletedImage.URL,
                    ClothesId = deletedImage.ClothesID,
                    IsDeleted = deletedImage.IsDeleted,
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
