﻿namespace ClothesShop.SharedVMs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public int ProductQuantity { get; set; }
    }
}
