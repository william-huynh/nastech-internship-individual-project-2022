using Microsoft.AspNetCore.Mvc;
using ClothesShop.SharedVMs.Categories;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace ClothesShop.CustomerSite.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7167/api");
        HttpClient httpClient;

        CategoriesReadDto _oCategory = new CategoriesReadDto();
        List<CategoriesReadDto> _oCategories = new List<CategoriesReadDto>();

        public CategoriesController()
        {
            httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            _oCategories = new List<CategoriesReadDto>();
            using (httpClient)
            {
                using (var response = await httpClient.GetAsync(baseAddress + "/Categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    #pragma warning disable CS8601 // Possible null reference assignment.
                    _oCategories = JsonConvert.DeserializeObject<List<CategoriesReadDto>>(apiResponse);
                    #pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
            return View(_oCategories);
        }
    }
}
