﻿namespace ClothesShop.API.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int RatingNumber { get; set; }
        public bool IsDelete { get; set; }
        public int ClothesID { get; set; }


        public virtual Clothes Clothes { get; set; }
    }
}
