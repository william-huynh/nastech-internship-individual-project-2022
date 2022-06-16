namespace ClotheShop.API.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int ClotheID { get; set; }
        public int CustomerID { get; set; }
        public int RatingNumber { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Clothe Clothe { get; set; }
    }
}
