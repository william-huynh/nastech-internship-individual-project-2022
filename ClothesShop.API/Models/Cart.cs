namespace ClothesShop.API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
