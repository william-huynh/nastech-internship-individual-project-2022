namespace ClotheShop.API.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string URL { get; set; }
        public int ClotheID { get; set; }

        public virtual Clothe Clothe { get; set; }
    }
}
