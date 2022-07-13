using AutoMapper;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;
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

        // GET (single): api/Images/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var image = await _image.GetByIdAsync(id);
                if (image == null)
                    return NotFound("Image not found!");
                var imageDto = _mapper.Map<ImageDto>(image);
                return Ok(imageDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostImage(ImageDto imageCreate)
        {
            try
            {
                var image = _mapper.Map<Image>(imageCreate);
                var imageCreated = await _image.PostAsync(image);
                return Ok(_mapper.Map<ImageDto>(imageCreated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // PUT: api/Images/{id}
        [HttpPut]
        public async Task<IActionResult> PutImage(ImageDto imageUpdate)
        {
            try
            {
                var imageChecked = await _image.GetByIdAsync(imageUpdate.Id);
                if (imageChecked == null)
                    return NotFound("Image not found!");
                var image = _mapper.Map<Image>(imageUpdate);
                var imageUpdated = await _image.PutAsync(image);
                return Ok(_mapper.Map<ImageDto>(imageUpdated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // DELETE: api/Images/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                var imageChecked = _image.GetByIdAsync(id);
                if (imageChecked == null)
                    return NotFound("Image not found!");
                await _image.DeleteAsync(id);
                return Ok("Image deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }
    }
}
