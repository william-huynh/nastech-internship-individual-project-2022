using ClothesShop.API.Data;
using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ClothesShop.CustomerSite.Controllers
{
    [ClothesShop.API.Authorization.Authorize(Role.Customer)]
    public class ClothesController : Controller
    {
        private readonly ClothesDbContext _context;

        // Clothes & List of Clothes
        ClothesDto clothes = new ClothesDto();
        List<ClothesDto> clothesList = new List<ClothesDto>();
        IClothesService clothesService = RestService.For<IClothesService>("https://localhost:7167/api");

        // Category & List of categories
        CategoryDto category = new CategoryDto();
        List<CategoryDto> categories = new List<CategoryDto>();
        ICategoriesService categoriesService = RestService.For<ICategoriesService>("https://localhost:7167/api");

        public ClothesController(ClothesDbContext context)
        {
            _context = context;
        }

        // GET (all) clothes
        public async Task<IActionResult> Index()
        {
            try
            {
                categories = await categoriesService.GetCategories();
                ViewBag.Categories = categories;
                clothesList = await clothesService.GetAllClothes();
                return View(clothesList);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET (all) clothes by category Id
        public async Task<IActionResult> Category(int id)
        {
            try
            {
                categories = await categoriesService.GetCategories();
                ViewBag.Categories = categories;
                clothesList = await clothesService.GetByCategoryId(id);
                return View(clothesList);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // Get (single) clothes
        public async Task<IActionResult> Single(int id)
        {
            try
            {
                List<ClothesDto> clothes = await clothesService.GetClothes(id);
                ViewBag.ClothesId = id;
                var ratings = _context.Ratings.Where(r => r.ClothesID.Equals(id)).ToList();
                if (ratings.Count() > 0)
                {
                    var ratingSum = ratings.Sum(d => d.RatingNumber);
                    ViewBag.RatingSum = ratingSum;
                    var ratingCount = ratings.Count();
                    ViewBag.RatingCount = ratingCount;
                }
                else
                {
                    ViewBag.RatingSum = 0;
                    ViewBag.RatingCount = 0;
                }

                return View(clothes);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
