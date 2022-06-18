namespace ClotheShop.API.Models
{
    public class Clothes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public int CategoryID { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual List<Image> Images { get; set; } = new List<Image>();
    }
}
