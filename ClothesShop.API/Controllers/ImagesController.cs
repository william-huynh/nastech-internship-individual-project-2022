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
        public static IWebHostEnvironment _webHostEnvironment;

        public ImagesController(IMapper mapper, IImageRepository image, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _image = image;
            _webHostEnvironment = webHostEnvironment;
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
                var imagesDto = _mapper.Map<List<ImageDto>>(images);
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
                var imageLink = _webHostEnvironment.WebRootPath + "\\uploads\\" + imageDto.URL;
                if (System.IO.File.Exists(imageLink))
                {
                    byte[] b = System.IO.File.ReadAllBytes(imageLink);
                    return File(b, "image/png");
                }
                return Ok(imageDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm]ImageDto imageCreate)
        {
            try
            {
                if (imageCreate.File.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    string fileURL = _webHostEnvironment.WebRootPath + "\\uploads\\" + imageCreate.File.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + imageCreate.File.FileName))
                    {
                        imageCreate.File.CopyTo(fileStream);
                        fileStream.Flush();
                        imageCreate.URL = imageCreate.File.FileName;
                        Console.WriteLine(imageCreate);
                        var image = _mapper.Map<Image>(imageCreate);
                        Console.WriteLine(image);
                        var imageCreated = await _image.PostAsync(image);
                        return Ok("Post finished! Image upload successfully!");
                    }
                }
                else
                {
                    return BadRequest("Image upload fail miserably!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // PUT: api/Images/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, [FromForm]ImageDto imageUpdate)
        {
            try
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fileStream = System.IO.File.Create(path + imageUpdate.File.FileName))
                {
                    imageUpdate.File.CopyTo(fileStream);
                    fileStream.Flush();
                    imageUpdate.URL = imageUpdate.File.FileName;
                    var image = _mapper.Map<Image>(imageUpdate);
                    var imageUpdated = await _image.PutAsync(id, image);
                    return Ok("Put finished! Successfully updated image!");
                }
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
