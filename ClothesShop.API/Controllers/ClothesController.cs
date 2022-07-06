using ClothesShop.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClothesShop.API.Controllers.ImagesController;
using AutoMapper;
using ClothesShop.SharedVMs;
using ClothesShop.API.Models;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly ClothesDbContext _context;
        private readonly IMapper _mapper;

        public ClothesController(ClothesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET (all): api/Clothes
        [HttpGet]
        public List<ClothesDto> GetAllClothes()
        {
            try
            {
                var allClothes = _mapper.Map<List<ClothesDto>>(
                    _context.Clothes.Where(clothe => clothe.IsDeleted == false));

                /* var allClothes = _context.Clothes.Include(clothes => clothes.Images).Select(clothes => new ClothesModel
                {
                    Id = clothes.ID,
                    Name = clothes.Name,
                    Description = clothes.Description,
                    Price = clothes.Price,
                    AddedDate = clothes.AddedDate,
                    CategoryId = clothes.CategoryId, 
                    IsDeleted = clothes.IsDeleted,
                    Images = clothes.Images.Select(images => new ImagesModel
                    {
                        Id = images.Id,
                        URL = images.URL,
                        ClothesId = images.ClothesID,
                    }).ToList()
                }).ToList(); */

                return allClothes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in GetAllClothes action: {ex.Message}");
                StatusCode(500, "Internal server error");
                return new List<ClothesDto>();
            }
        }

        // GET (single): api/Clothes/{id}
        [HttpGet("{id}")]
        public ClothesDto GetClothes(int id)
        {
            try
            {
                var checkedClothes = _context.Clothes.Find(id);
                if (checkedClothes == null || checkedClothes.IsDeleted == true)
                {
                    StatusCode(404, "Clothes not found");
                    return new ClothesDto();
                }
                var singleClothes = _mapper.Map<ClothesDto>(checkedClothes);
                return singleClothes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in GetSingleClothes action: {ex.Message}");
                StatusCode(500, "Internal server error");
                return new ClothesDto();
            }
        }

        // POST: api/Clothes
        [HttpPost]
        public IActionResult PostClothes(ClothesDto clothesCreate)
        {
            try
            {
                var newClothes = _mapper.Map<Clothes>(clothesCreate);
                newClothes.IsDeleted = false;
                newClothes.AddedDate = DateTime.UtcNow;
                newClothes.UpdatedDate = DateTime.UtcNow;
                _context.Clothes.Add(newClothes);
                _context.SaveChanges();
                return Ok(_mapper.Map<ClothesDto>(newClothes));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in PostClothes action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Clothes/{id}
        [HttpPut("{id}")]
        public ClothesDto PutClothes(int id, ClothesDto clothesUpdate)
        {
            try
            {
                var updatedClothes = _context.Clothes.Find(id);
                if (updatedClothes == null || updatedClothes.IsDeleted == true)
                {
                    StatusCode(404, "Clothes not found");
                    return new ClothesDto();
                }
                updatedClothes.Name = clothesUpdate.Name;
                updatedClothes.Description = clothesUpdate.Description;
                updatedClothes.Price = clothesUpdate.Price;
                updatedClothes.UpdatedDate = DateTime.UtcNow;
                updatedClothes.CategoryId = clothesUpdate.CategoryId;
                _context.SaveChanges();
                return _mapper.Map<ClothesDto>(updatedClothes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in PutClothes action: {ex.Message}");
                StatusCode(500, "Internal server error");
                return new ClothesDto();
            }
        }

        // DELETE: api/Clothes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteClothes(int id)
        {
            try
            {
                var deletedClothes = _context.Clothes.Find(id);
                if (deletedClothes == null)
                {
                    return StatusCode(404, "Clothes not found");
                }
                deletedClothes.IsDeleted = true;
                _context.SaveChanges();
                return Ok(_mapper.Map<ClothesDto>(deletedClothes));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in DeleteClothes action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
