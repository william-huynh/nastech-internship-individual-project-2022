using ClothesShop.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClothesShop.API.Controllers.ImagesController;
using AutoMapper;
using ClothesShop.SharedVMs;
using ClothesShop.API.Models;
using ClothesShop.API.Interfaces;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly ClothesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IClothesRepository _clothes;

        public ClothesController(ClothesDbContext context, IMapper mapper, IClothesRepository clothes)
        {
            _context = context;
            _mapper = mapper;
            _clothes = clothes;
        }

        // GET (all): api/Clothes
        [HttpGet]
        public async Task<IActionResult> GetAllClothes()
        {
            try
            {
                var clothes = await _clothes.GetAsync();
                if (!clothes.Any())
                    return NotFound("Clothes empty!");
                var clothesDto = _mapper.Map<List<ClothesDto>>(clothes.Where(c => c.IsDeleted.Equals(false)));
                return Ok(clothesDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // GET (single): api/Clothes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClothes(int id)
        {
            try
            {
                var clothes = await _clothes.GetByIdAsync(id);
                if (clothes == null || clothes.IsDeleted.Equals(true))
                    return NotFound("Clothes not found!");
                var clothesDto = _mapper.Map<ClothesDto>(clothes);
                return Ok(clothesDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // POST: api/Clothes
        [HttpPost]
        public async Task<IActionResult> PostClothes(ClothesDto clothesCreate)
        {
            try
            {
                var clothes = _mapper.Map<Clothes>(clothesCreate);
                clothes.AddedDate = DateTime.UtcNow;
                clothes.UpdatedDate = DateTime.UtcNow;
                var clothesCreated = await _clothes.PostAsync(clothes);
                return Ok(_mapper.Map<ClothesDto>(clothesCreated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // PUT: api/Clothes/{id}
        [HttpPut]
        public async Task<IActionResult> PutClothes(ClothesDto clothesUpdate)
        {
            try
            {
                var clothesChecked = await _clothes.GetByIdAsync(clothesUpdate.ID);
                if (clothesChecked == null || clothesChecked.IsDeleted.Equals(true))
                    return NotFound("Clothes not found!");
                var clothes = _mapper.Map<Clothes>(clothesUpdate);
                clothes.AddedDate = clothesChecked.AddedDate;
                clothes.UpdatedDate = DateTime.UtcNow;
                var clothesUpdated = await _clothes.PutAsync(clothes);
                return Ok(_mapper.Map<ClothesDto>(clothesUpdated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // DELETE: api/Clothes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClothes(int id)
        {
            try
            {
                var clothesChecked = await _clothes.GetByIdAsync(id);
                if (clothesChecked == null || clothesChecked.IsDeleted.Equals(true))
                    return NotFound("Clothes not found!");
                await _clothes.DeleteAsync(id);
                return Ok("Clothes deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }
    }
}
