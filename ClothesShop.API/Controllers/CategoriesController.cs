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
        public IActionResult GetAllCategories()
        {
            try
            {
                var allCategories = _context.Categories.Select(c => new CategoriesModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsDeleted = c.IsDeleted,
                }).ToList();
                return Ok(allCategories.Where(ac => ac.IsDeleted == false));
            }
            catch
            {
                return BadRequest();
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
                    return NotFound();
                }
                var singleCategory = new CategoriesModel
                {
                    Id = checkedCategory.Id,
                    Name = checkedCategory.Name,
                    Description = checkedCategory.Description,
                    IsDeleted = checkedCategory.IsDeleted,
                };
                return Ok(singleCategory);
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult PostCategory(CategoriesModel categoryModel)
        {
            try
            {
                var newCategory = new Category
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
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Categories/{id}
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, CategoriesModel categoryModel)
        {
            try
            {
                var updatedCategory = _context.Categories.Find(id);
                if (updatedCategory == null)
                {
                    return NotFound();
                }
                updatedCategory.Name = categoryModel.Name;
                updatedCategory.Description = categoryModel.Description;
                _context.SaveChanges();
                return Ok(new CategoriesModel
                {
                    Id = updatedCategory.Id,
                    Name = updatedCategory.Name,
                    Description = updatedCategory.Description,
                    IsDeleted = updatedCategory.IsDeleted,
                });
            }
            catch
            {
                return BadRequest();
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
                    return NotFound();
                }
                deletedCategory.IsDeleted = true;
                _context.SaveChanges();
                return Ok(new CategoriesModel
                {
                    Id = deletedCategory.Id,
                    Name = deletedCategory.Name,
                    Description = deletedCategory.Description,
                    IsDeleted = deletedCategory.IsDeleted,
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        // Categories POCO model (for testing)
        public class CategoriesModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsDeleted { get; set; } = false;
        }
    }
}
