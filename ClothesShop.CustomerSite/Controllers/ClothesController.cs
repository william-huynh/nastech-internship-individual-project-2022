using ClothesShop.API.Data;
using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.IdentityModel.Tokens.Jwt;

namespace ClothesShop.CustomerSite.Controllers
{
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

        IRatingsService ratingsService = RestService.For<IRatingsService>("https://localhost:7167/api");

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
                var token = HttpContext.Session.GetString("Token");
                var handler = new JwtSecurityTokenHandler();
                if (token != null)
                {
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var userId = tokenS.Claims.First(claim => claim.Type == "Username").Value;
                    ViewBag.Username = userId;
                }
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
                var token = HttpContext.Session.GetString("Token");
                var handler = new JwtSecurityTokenHandler();
                if (token != null)
                {
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var userId = tokenS.Claims.First(claim => claim.Type == "Username").Value;
                    ViewBag.Username = userId;
                }
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
                var token = HttpContext.Session.GetString("Token");
                var handler = new JwtSecurityTokenHandler();
                if (token != null)
                {
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var user = tokenS.Claims.First(claim => claim.Type == "Username").Value;
                    int userId = Int32.Parse(tokenS.Claims.First(claim => claim.Type == "Id").Value);
                    var userRated = await ratingsService.GetRatingByUser(id, userId);
                    ViewBag.Username = user;
                    ViewBag.UserId = userId;
                    ViewBag.UserRating = userRated.RatingNumber;
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
