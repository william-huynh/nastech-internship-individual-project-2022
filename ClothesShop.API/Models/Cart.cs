using ClotheShop.API.Models;

namespace ClothesShop.API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
