namespace ClothesShop.SharedVMs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int RatingNumber { get; set; }
        public bool IsDelete { get; set; }
        public int ClothesID { get; set; }
        public int UsersId { get; set; }
    }
}
