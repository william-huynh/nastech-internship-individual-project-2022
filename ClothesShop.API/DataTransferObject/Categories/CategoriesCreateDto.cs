namespace ClothesShop.API.DataTransferObject.Categories
{
    public class CategoriesCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public int ProductQuantity { get; set; }
    }
}
