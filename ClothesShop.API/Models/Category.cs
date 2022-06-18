using System.ComponentModel.DataAnnotations;

namespace ClotheShop.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductQuantity { get; set; }

        public virtual List<Clothes> Clothes { get; set; } = new List<Clothes>();
    }
}
