using AutoMapper;
using ClothesShop.API.Data;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
