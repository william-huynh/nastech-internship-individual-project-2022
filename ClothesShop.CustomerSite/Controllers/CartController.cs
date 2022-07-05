using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.CustomerSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
