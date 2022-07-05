using ClotheShop.CustomerSite.Models;
using ClothesShop.API.Authorization;
using ClothesShop.API.Services;
using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Authenticate;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ClotheShop.CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        // Category & List of categories
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
                if (response == null) return RedirectToAction("Error");
                HttpContext.Session.SetString("Token", response.Token);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
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
                if (response == null) return RedirectToAction("Error");
                return RedirectToAction("Login");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        [ClothesShop.API.Authorization.Authorize(Role.Customer)]
        [Route("index")]
        [HttpGet]
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
    }
}