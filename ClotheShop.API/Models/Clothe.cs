namespace ClotheShop.API.Models
{
    public class Clothe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual Category Category { get; set; }
    }
}
