using Microsoft.AspNetCore.Mvc;
using ClothesShop.SharedVMs;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Text;
using ClothesShop.API.Models;
using Refit;
using ClothesShop.CustomerSite.Services;

namespace ClothesShop.CustomerSite.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7167/api");
        HttpClient httpClient;

        // Category & List of categories
        CategoryDto category = new CategoryDto();
        List<CategoryDto> categories = new List<CategoryDto>();
        ICategoriesService categoriesService = RestService.For<ICategoriesService>("https://localhost:7167/api");

        public CategoriesController()
        {
            httpClient = new HttpClient();
        }

        //public ViewResult Index() => View();

        // GET (all) categories
        public async Task<IActionResult> Index()
        {
            try
            {
                categories = await categoriesService.GetCategories();
                return View(categories);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET (single) category
        public ViewResult Single() => View();

        [HttpPost]
        public async Task<IActionResult> Single(int id)
        {
            try
            {
                category = await categoriesService.GetCategory(id);
                return View(category);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // POST category
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto _oCategoryCreate)
        {
            try
            {
                await categoriesService.CreateCategory(_oCategoryCreate);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // PUT category
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                category = await categoriesService.GetCategory(id);
                return View(category);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryDto _oCategoryUpdate)
        {
            try
            {
                await categoriesService.UpdateCategory(id, _oCategoryUpdate);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // DELETE category
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await categoriesService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // Error 
        public ViewResult Error() => View();
    }
}
