using ClothesShop.API.Data;
using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ClothesShop.CustomerSite.Controllers
{
    public class ClothesController : Controller
    {
        private readonly ClothesDbContext _context;

        // Clothes & List of Clothes
        ClothesDto clothes = new ClothesDto();
        List<ClothesDto> clothesList = new List<ClothesDto>();
        IClothesService clothesService = RestService.For<IClothesService>("https://localhost:7167/api");

        public ClothesController(ClothesDbContext context)
        {
            _context = context;
        }

        // GET (all) clothes
        public IActionResult Index()
        {
            return View();
        }

        // Get (single) clothes
        public async Task<IActionResult> Single(int id)
        {
            ClothesDto clothes = await clothesService.GetClothes(id);
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
    }
}
