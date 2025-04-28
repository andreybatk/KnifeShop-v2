﻿namespace KnifeShop.DB.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<KnifeCategory> KnifeCategories { get; set; } = [];
    }
}
