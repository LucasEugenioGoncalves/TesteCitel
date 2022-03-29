namespace TesteCitel.Domain.Arguments.Category
{
    public class CategoryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static explicit operator CategoryResponse(Entities.Category category)
        {
            if (category is null) return null;
            return new CategoryResponse
            {
                Id = category.Id,               
                Name = category.Name,
            };
        }
    }
}
