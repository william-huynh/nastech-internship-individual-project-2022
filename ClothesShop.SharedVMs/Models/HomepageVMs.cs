using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.SharedVMs.Models
{
    public class HomepageVMs
    {
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public CategoryDto Category { get; set; } = new CategoryDto();
        public List<ClothesDto> Clothes { get; set; } = new List<ClothesDto>();
    }
}
