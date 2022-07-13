using Microsoft.AspNetCore.Http;

namespace ClothesShop.SharedVMs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string URL { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public IFormFile File { get; set; }
        public int ClothesId { get; set; }
    }
}
