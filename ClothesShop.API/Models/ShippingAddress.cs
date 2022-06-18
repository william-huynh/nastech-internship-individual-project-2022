using ClotheShop.API.Models;

namespace ClothesShop.API.Models
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int Zipcode { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
