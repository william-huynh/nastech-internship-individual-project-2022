namespace ClothesShop.API.DataTransferObject.Categories
{
    public class CategoriesUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProductQuantity { get; set; }
    }
}
