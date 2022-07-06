namespace ClothesShop.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } 

        public virtual List<Clothes> Clothes { get; set; } = new List<Clothes>();
    }
}
