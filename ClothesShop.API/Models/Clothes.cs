namespace ClothesShop.API.Models
{
    public class Clothes
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Image> Images { get; set; } = new List<Image>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
