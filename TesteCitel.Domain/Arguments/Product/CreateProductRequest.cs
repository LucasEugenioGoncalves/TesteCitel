﻿namespace TesteCitel.Domain.Arguments.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}
