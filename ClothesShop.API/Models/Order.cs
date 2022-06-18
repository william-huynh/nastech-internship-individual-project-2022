using ClotheShop.API.Models;

namespace ClothesShop.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ShippingAddress ShippingAddress { get; set; }
        public virtual BillingAddress BillingAddress { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
