﻿namespace ClotheShop.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Clothe> Clothes { get; set; } = new List<Clothe>();
    }
}
