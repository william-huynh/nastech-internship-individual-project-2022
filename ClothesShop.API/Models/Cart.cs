using ClothesShop.API.Models;

namespace ClothesShop.API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User Customer { get; set; }
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
