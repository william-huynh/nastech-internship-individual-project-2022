namespace ClothesShop.SharedVMs
{
    public class ClothesListDto
    {
        public List<ClothesDto> Clothes { get; set; }
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int? PageSize { get; set; }

        // Khoi tao list de tranh null
        public ClothesListDto()
        {
            this.Clothes = new List<ClothesDto>();
        }
    }
}
