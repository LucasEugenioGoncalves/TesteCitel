namespace TesteCitel.Domain.Arguments.Product
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        public static explicit operator ProductResponse(Entities.Product product)
        {
            if (product is null) return null;
            return new ProductResponse
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price.ToString(),
                CategoryName = product?.Category?.Name
            }; 
        }
    }
}
