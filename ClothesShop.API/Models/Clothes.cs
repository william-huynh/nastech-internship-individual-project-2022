using ClothesShop.API.Models;

namespace ClotheShop.API.Models
{
    public class Clothes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Image> Images { get; set; } = new List<Image>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
