using AutoMapper;
using ClothesShop.API.Interfaces;
using ClothesShop.SharedVMs;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _image;

        public ImagesController(IMapper mapper, IImageRepository image)
        {
            _mapper = mapper;
            _image = image;
        }

        // GET (all): api/Images
        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var images = await _image.GetAsync();
                if (!images.Any())
                    return NotFound("Images empty!");
                var imagesDto = _mapper.Map<ImageDto>(images);
                return Ok(imagesDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }
    }
}
