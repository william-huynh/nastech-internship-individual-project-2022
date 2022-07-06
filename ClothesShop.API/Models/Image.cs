namespace ClothesShop.API.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public int ClothesID { get; set; }

        public virtual Clothes Clothes { get; set; }
    }
}
