using System;

namespace TesteCitel.Domain.Arguments.Product
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        public static explicit operator ProductResponse(Entities.Product v)
        {
            throw new NotImplementedException();
        }
    }
}
