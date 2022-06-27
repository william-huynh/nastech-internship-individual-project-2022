namespace ClothesShop.SharedVMs.Categories
{
    public class CategoriesUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProductQuantity { get; set; }
    }
}
