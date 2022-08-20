namespace ClothesShop.SharedVMs
{
    public class CategoriesListDto
    {
        public List<CategoryDto> Categories { get; set; }
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int? PageSize { get; set; }

        // Khoi tao list de tranh null
        public CategoriesListDto()
        {
            this.Categories = new List<CategoryDto>();
        }
    }
}
