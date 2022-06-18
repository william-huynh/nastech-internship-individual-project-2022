namespace ClotheShop.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime SubscribedDate { get; set; }
    }
}
