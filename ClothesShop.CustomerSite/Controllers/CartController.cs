using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.CustomerSite.Controllers
{
    [ClothesShop.API.Authorization.Authorize(Role.Customer)]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
