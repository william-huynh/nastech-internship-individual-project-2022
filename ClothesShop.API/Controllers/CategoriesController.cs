﻿using AutoMapper;
using ClotheShop.API.Data;
using ClotheShop.API.Models;
using ClothesShop.API.DataTransferObject.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ClothesDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ClothesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET (all): api/Categories
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var allCategories = _mapper.Map<List<CategoriesReadDto>>(
                    _context.Categories.Where(category => category.IsDeleted == false));    // Map data using Auto Mapper

                /* var allCategories = _context.Categories.Select(c => new CategoriesModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsDeleted = c.IsDeleted,
                }).ToList(); */

                return Ok(allCategories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in GetAllCategories action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET (single): api/Categories/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var checkedCategory = _context.Categories.Find(id);
                if (checkedCategory == null || checkedCategory.IsDeleted == true)
                {
                    return NotFound("Category not found!");
                }
                var singleCategory = _mapper.Map<CategoriesReadDto>(checkedCategory);   // Map data using Auto Mapper

                /* var singleCategory = new CategoriesModel
                {
                    Id = checkedCategory.Id,
                    Name = checkedCategory.Name,
                    Description = checkedCategory.Description,
                    IsDeleted = checkedCategory.IsDeleted,
                }; */

                return Ok(singleCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in GetCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult PostCategory(CategoriesCreateDto categoryCreate)
        {
            try
            {
                var newCategory = _mapper.Map<Category>(categoryCreate);    // Use Auto Mapper => categoryCreate to Category
                newCategory.IsDeleted = false;
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                return Ok(_mapper.Map<CategoriesReadDto>(newCategory));     // Use Auto Mapper => newCategory to CategoriesReadDto

                /* var newCategory = new Category
                {
                    Name = categoryModel.Name,
                    Description = categoryModel.Description,
                };
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                return Ok(new CategoriesModel
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    Description = newCategory.Description,
                    IsDeleted = newCategory.IsDeleted,
                }); */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in PostCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, CategoriesUpdateDto categoryUpdate)
        {
            try
            {
                var updatedCategory = _context.Categories.Find(id);
                if (updatedCategory == null || updatedCategory.IsDeleted == true)
                {
                    return NotFound("Category not found!");
                }
                updatedCategory.Name = categoryUpdate.Name;
                updatedCategory.Description = categoryUpdate.Description;
                updatedCategory.ProductQuantity = categoryUpdate.ProductQuantity;
                _context.SaveChanges();
                return Ok(_mapper.Map<CategoriesReadDto>(updatedCategory)); // Use Auto Mapper => updatedCategory to CategoriesReadDto
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in PutCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var deletedCategory = _context.Categories.Find(id);
                if (deletedCategory == null)
                {
                    return NotFound("Category not found!");
                }
                deletedCategory.IsDeleted = true;
                _context.SaveChanges();
                return Ok(_mapper.Map<CategoriesReadDto>(deletedCategory)); // Use Auto Mapper => deletedCategory to CategoriesReadDto
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went very wrong in DeleteCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Categories POCO model (for testing)
        /* public class CategoriesModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsDeleted { get; set; } = false;
        } */
    }
}
