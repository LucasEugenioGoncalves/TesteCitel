namespace TesteCitel.Domain.Arguments.Product
{
    public class UpdateProductRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}
