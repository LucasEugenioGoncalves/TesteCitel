using System;

namespace TesteCitel.Domain.Arguments.Category
{
    public class CategoryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static explicit operator CategoryResponse(Entities.Category v)
        {
            throw new NotImplementedException();
        }
    }
}
