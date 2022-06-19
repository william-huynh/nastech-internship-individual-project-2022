using ClotheShop.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ClothesDbContext _context;

        public ImagesController(ClothesDbContext context)
        {
            _context = context;
        }

        // GET (all): api/Images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
