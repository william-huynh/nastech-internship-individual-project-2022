using Microsoft.AspNetCore.Mvc;
using ClothesShop.SharedVMs.Categories;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Text;

namespace ClothesShop.CustomerSite.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7167/api");
        HttpClient httpClient;

        // Read category/categories
        CategoriesReadDto _oCategoryRead = new CategoriesReadDto();
        List<CategoriesReadDto> _oCategoriesRead = new List<CategoriesReadDto>();

        // Create category
        CategoriesCreateDto _oCategoryCreate = new CategoriesCreateDto();

        public CategoriesController()
        {
            httpClient = new HttpClient();
        }

        // GET (all) categories
        public async Task<IActionResult> Index()
        {
            _oCategoriesRead = new List<CategoriesReadDto>();
            using (httpClient)
            {
                using (var response = await httpClient.GetAsync(baseAddress + "/Categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    #pragma warning disable CS8601 // Possible null reference assignment.
                    _oCategoriesRead = JsonConvert.DeserializeObject<List<CategoriesReadDto>>(apiResponse);
                    #pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
            return View(_oCategoriesRead);
        }

        // GET (single) category
        public ViewResult Single() => View();

        [HttpPost]
        public async Task<IActionResult> Single(int id)
        {
            _oCategoryRead = new CategoriesReadDto();
            using (httpClient)
            {
                using (var response = await httpClient.GetAsync(baseAddress + "/Categories/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        #pragma warning disable CS8601 // Possible null reference assignment.
                        _oCategoryRead = JsonConvert.DeserializeObject<CategoriesReadDto>(apiResponse);
                        #pragma warning restore CS8601 // Possible null reference assignment.
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View(_oCategoryRead);
        }

        // POST category
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoriesCreateDto _categoryCreate)
        {
            _oCategoryRead = new CategoriesReadDto();
            using (httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_categoryCreate), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(baseAddress + "/Categories", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    #pragma warning disable CS8601 // Possible null reference assignment.
                    _oCategoryRead = JsonConvert.DeserializeObject<CategoriesReadDto>(apiResponse);
                    #pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
            return View(_oCategoryRead);
        }
    }
}
