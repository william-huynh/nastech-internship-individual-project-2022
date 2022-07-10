using ClothesShop.SharedVMs;
using Refit;

namespace ClothesShop.CustomerSite.Services
{
    public interface IRatingsService
    {
        // Get all ratings
        /* [Get("/Ratings")]
        Task<List<RatingDto>> GetRatings();

        // Get rating by Id
        [Get("/Ratings/{id}")]
        Task<RatingDto> GetRating(int id); */

        // Add rating
        [Post("/Ratings")]
        Task CreateRating([Body] RatingDto rating);

        // Update rating
        /*[Put("/Ratings")]
        Task UpdateRating([Body] RatingDto rating);

        // Delete rating
        [Delete("/Ratings/{id}")]
        Task DeleteRating(int id); */
    }
}
