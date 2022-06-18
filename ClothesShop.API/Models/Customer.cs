using ClothesShop.API.Models;

namespace ClotheShop.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; }
        public int CartId { get; set; }
        public int OrderId { get; set; }
        public int RatingId { get; set; }

        public virtual User user { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ShippingAddress ShippingAddress { get; set; }
        public virtual BillingAddress BillingAddress { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
