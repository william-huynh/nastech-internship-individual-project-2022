using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.CustomerSite.Controllers
{
    public class ClothesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Single()
        {
            return View();
        }
    }
}
