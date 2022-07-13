namespace ClothesShop.SharedVMs
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string URL { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public int ClothesId { get; set; }
    }
}
