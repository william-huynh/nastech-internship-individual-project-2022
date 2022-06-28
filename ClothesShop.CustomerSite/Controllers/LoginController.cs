using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.CustomerSite.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            return View();
        }
    }
}
