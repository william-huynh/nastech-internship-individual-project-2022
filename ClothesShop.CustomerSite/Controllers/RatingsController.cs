using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Microsoft.AspNetCore.Authorization;

namespace ClothesShop.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        // Rating service
        IRatingsService ratingsService;

        public RatingsController()
        {
            ratingsService = RestService.For<IRatingsService>("https://localhost:7167/api", new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(this.HttpContext.Session.GetString("Token"))
            });
        }

        // POST rating
        [HttpPost]
        public async Task<IActionResult> Create(RatingDto ratingCreate)
        {
            try
            {
                await ratingsService.CreateRating(ratingCreate);
                return RedirectToAction("Single", "Clothes", new { id = ratingCreate.ClothesID });
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
