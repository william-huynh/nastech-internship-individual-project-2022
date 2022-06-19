namespace ClotheShop.API.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public bool IsDeleted { get; set; }
        public int ClothesID { get; set; }

        public virtual Clothes Clothes { get; set; }
    }
}
