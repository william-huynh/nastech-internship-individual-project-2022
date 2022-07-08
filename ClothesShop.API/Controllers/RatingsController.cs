using AutoMapper;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
using ClothesShop.SharedVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRatingRepository _rating;

        public RatingsController(IMapper mapper, IRatingRepository rating)
        {
            _mapper = mapper;
            _rating = rating;
        }

        // GET (all): api/Ratings
        [HttpGet]
        public async Task<IActionResult> GetRatings()
        {
            try
            {
                var ratings = await _rating.GetAsync();
                if (!ratings.Any()) 
                    return NotFound("Ratings empty!");
                var ratingsDto = _mapper.Map<List<RatingDto>>(ratings.Where(r => r.IsDelete == false));
                return Ok(ratingsDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Get (single): api/Ratings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            try
            {
                var rating = await _rating.GetByIdAsync(id);
                if (rating == null || rating.IsDelete == true) 
                    return NotFound("Rating not found!");
                var ratingDto = _mapper.Map<RatingDto>(rating);
                return Ok(ratingDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Post: api/Ratings
        [HttpPost]
        public async Task<IActionResult> PostRating(RatingDto ratingCreate)
        {
            try
            {
                var rating = _mapper.Map<Rating>(ratingCreate);
                var ratingCreated = await _rating.PostAsync(rating);
                return Ok(_mapper.Map<RatingDto>(ratingCreated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Put: api/Ratings/{id}
        [HttpPut]
        public async Task<IActionResult> PutRating(RatingDto ratingUpdate)
        {
            try
            {
                var ratingChecked = await _rating.GetByIdAsync(ratingUpdate.Id);
                if (ratingChecked == null || ratingChecked.IsDelete == true)
                    return NotFound("Rating not found!");
                var rating = _mapper.Map<Rating>(ratingUpdate);
                var ratingUpdated = await _rating.PutAsync(rating);
                return Ok(_mapper.Map<RatingDto>(ratingUpdated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Delete: api/Ratings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            try
            {
                var ratingChecked = await _rating.GetByIdAsync(id);
                if (ratingChecked == null || ratingChecked.IsDelete == true)
                    return NotFound("Rating not found!");
                await _rating.DeleteAsync(id);
                return Ok("Rating deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }
    }
}
