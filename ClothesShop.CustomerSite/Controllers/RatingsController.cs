using AutoMapper;
using ClothesShop.API.Data;
using ClothesShop.API.Models;
using ClothesShop.CustomerSite.Services;
using ClothesShop.SharedVMs;
using ClothesShop.SharedVMs.Enum;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ClothesShop.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        // Rating service
        IRatingsService ratingsService = RestService.For<IRatingsService>("https://localhost:7167/api");

        // POST rating
        [API.Authorization.Authorize(Role.Customer)]
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
