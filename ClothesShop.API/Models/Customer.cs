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

        public User user { get; set; }
        public Cart Cart { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
