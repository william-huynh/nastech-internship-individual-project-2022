using ClotheShop.CustomerSite.Models;
using ClothesShop.API.Authorization;
using ClothesShop.API.Services;
using ClothesShop.SharedVMs.Authenticate;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClotheShop.CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Login() => View();

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(AuthenticateRequestDto authenticateRequest)
        {
            var response = _userService.Authenticate(authenticateRequest);
            if (response == null) return RedirectToAction("Error");
            HttpContext.Session.SetString("Token", response.Token);
            return RedirectToAction("Index");
        }

        [Authorize(Role.Customer)]
        [Route("index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
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