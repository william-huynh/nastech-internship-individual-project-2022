﻿using ClotheShop.API.Models;
using ClothesShop.API.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ClothesShop.API.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
