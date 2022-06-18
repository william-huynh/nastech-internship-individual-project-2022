using ClotheShop.API.Models;

namespace ClothesShop.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public int ClothesId { get; set; }
        public int OrderId { get; set; }

        public virtual Clothes Clothes { get; set; }
        public virtual Order Order { get; set; }
    }
}
