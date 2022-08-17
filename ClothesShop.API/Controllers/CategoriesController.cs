using AutoMapper;
using ClothesShop.API.Authorization;
using ClothesShop.API.Data;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _category;

        public CategoriesController(IMapper mapper, ICategoryRepository category)
        {
            _mapper = mapper;
            _category = category;
        }

        // GET (all): api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _category.GetAsync();
                if (!categories.Any())
                    return NotFound("Categories empty!");
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // GET (all): api/Categories/Quantity
        [HttpGet("Quantity")]
        public async Task<IActionResult> Get5Categories()
        {
            try
            {
                var categories = await _category.Get5Async();
                if (!categories.Any())
                    return NotFound("Categories empty!");
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // GET (single): api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _category.GetByIdAsync(id);
                if (category == null)
                    return NotFound("Category not found!");
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryDto categoryCreate)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryCreate);
                var categoryCreated = await _category.PostAsync(category);
                return Ok(_mapper.Map<CategoryDto>(categoryCreated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // PUT: api/Categories/{id}
        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoryDto categoryUpdate)
        {
            try
            {
                var categoryChecked = await _category.GetByIdAsync(categoryUpdate.Id);
                if (categoryChecked == null )
                    return NotFound("Category not found!");
                var category = _mapper.Map<Category>(categoryUpdate);
                var categoryUpdated = await _category.PutAsync(category);
                return Ok(_mapper.Map<CategoryDto>(categoryUpdated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var categoryChecked = await _category.GetByIdAsync(id);
                if (categoryChecked == null || categoryChecked.IsDeleted.Equals(true))
                    return NotFound("Category not found!");
                await _category.DeleteAsync(id);
                return Ok("Category deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }
    }
}
