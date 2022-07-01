using Microsoft.AspNetCore.Mvc;
using ClothesShop.SharedVMs;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Text;
using ClothesShop.API.Models;

namespace ClothesShop.CustomerSite.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7167/api");
        HttpClient httpClient;

        // Read category/categories
        CategoryDto _oCategoryRead = new CategoryDto();
        List<CategoryDto> _oCategoriesRead = new List<CategoryDto>();

        // Create category
        CategoryDto _oCategoryCreate = new CategoryDto();

        public CategoriesController()
        {
            httpClient = new HttpClient();
        }

        // GET (all) categories
        public async Task<IActionResult> Index()
        {
            _oCategoriesRead = new List<CategoryDto>();
            using (httpClient)
            {
                using (var response = await httpClient.GetAsync(baseAddress + "/Categories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    #pragma warning disable CS8601 // Possible null reference assignment.
                    _oCategoriesRead = JsonConvert.DeserializeObject<List<CategoryDto>>(apiResponse);
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
            _oCategoryRead = new CategoryDto();
            using (httpClient)
            {
                using (var response = await httpClient.GetAsync(baseAddress + "/Categories/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        #pragma warning disable CS8601 // Possible null reference assignment.
                        _oCategoryRead = JsonConvert.DeserializeObject<CategoryDto>(apiResponse);
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
        public async Task<IActionResult> Create(CategoryDto _categoryCreate)
        {
            _oCategoryRead = new CategoryDto();
            using (httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_categoryCreate), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(baseAddress + "/Categories", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    #pragma warning disable CS8601 // Possible null reference assignment.
                    _oCategoryRead = JsonConvert.DeserializeObject<CategoryDto>(apiResponse);
                    #pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
            return View(_oCategoryRead);
        }

        // PUT category
        public async Task<IActionResult> Update(int id)
        {
            _oCategoryRead = new CategoryDto();
            using (httpClient)
            {
                using (var response = await httpClient.GetAsync(baseAddress + "/Categories/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        #pragma warning disable CS8601 // Possible null reference assignment.
                        _oCategoryRead = JsonConvert.DeserializeObject<CategoryDto>(apiResponse);
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

        [HttpPost]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryDto _categoryUpdate)
        {
            _oCategoryRead = new CategoryDto();
            using (httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_categoryUpdate), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(baseAddress + "/Categories/" + id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";

                    #pragma warning disable CS8601 // Possible null reference assignment.
                    _oCategoryRead = JsonConvert.DeserializeObject<CategoryDto>(apiResponse);
                    #pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
            return View(_oCategoryRead);
        }

        // DELETE category
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (httpClient)
            {
                using (var response = await httpClient.DeleteAsync(baseAddress + "/Categories/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
