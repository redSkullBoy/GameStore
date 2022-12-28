﻿using GameStore.Data.Entities;

namespace GameStore.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
            = Enumerable.Empty<Product>();

        public PagingInfo PagingInfo { get; set; } = new();
        public string? CurrentCategory { get; set; }
        public string? Search { get; set; }
    }
}
