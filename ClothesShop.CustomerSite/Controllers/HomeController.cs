using ClotheShop.CustomerSite.Models;
using ClothesShop.API.Authorization;
using ClothesShop.API.Services;
using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Authenticate;
using ClothesShop.SharedVMs.Enum;
using ClothesShop.SharedVMs.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Dynamic;

namespace ClotheShop.CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        // Clothes & List of Clothes
        ClothesDto clothes = new ClothesDto();
        List<ClothesDto> clothesList = new List<ClothesDto>();
        IClothesService clothesService = RestService.For<IClothesService>("https://localhost:7167/api");

        // Category & List of categories
        CategoryDto category = new CategoryDto();
        List<CategoryDto> categories = new List<CategoryDto>();
        ICategoriesService categoriesService = RestService.For<ICategoriesService>("https://localhost:7167/api");

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login() => View();

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(AuthenticateRequestDto authenticateRequest)
        {
            try 
            { 
                var response = _userService.Authenticate(authenticateRequest);
                if (response == null) return RedirectToAction("ErrorLogin");
                HttpContext.Session.SetString("Token", response.Token);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("ErrorLogin");
            }
        }

        public IActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterRequestDto registerRequest)
        {
            try
            {
                var response = _userService.Register(registerRequest);
                if (response == null) return RedirectToAction("ErrorLogin");
                return RedirectToAction("Login");
            }
            catch
            {
                return RedirectToAction("ErrorLogin");
            }
        }

        [ClothesShop.API.Authorization.Authorize(Role.Customer)]
        [Route("index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                HomepageVMs homepageVMs = new HomepageVMs();
                categories = await categoriesService.GetCategories();
                homepageVMs.Categories = categories;
                clothesList = await clothesService.GetAllClothes();
                homepageVMs.Clothes = clothesList;
                return View(homepageVMs);
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ErrorLogin()
        {
            return View();
        }
    }
}