using ClotheShop.API.Models;
using ClothesShop.API.Models.Enum;

namespace ClothesShop.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<OrderItem> CartItems { get; set; } = new List<OrderItem>();
    }
}
