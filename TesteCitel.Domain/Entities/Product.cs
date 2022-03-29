using prmToolkit.NotificationPattern;
using TesteCitel.Domain.Entities.Base;

namespace TesteCitel.Domain.Entities
{
    public class Product : EntityBase
    {
        public Product(string name, decimal price, string categoryId)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;

            new AddNotifications<Product>(this).IfNullOrEmpty(x => x.Name, "O nome é obrigatório.");
            new AddNotifications<Product>(this).IfNullOrInvalidLength(x => x.Name, 1, 50, "O nome deve conter de 1 á 50 caracteres.");
            new AddNotifications<Product>(this).IfEqualsZero(x => x.Price, "O preço não pode ser igual a zero.");
            new AddNotifications<Product>(this).IfNullOrEmpty(x => x.CategoryId, "A categoria é obrigatório.");
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string CategoryId { get; private set; }
        public virtual Category Category { get; set; }
        public void Update(string name, decimal price, string categoryId)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;

            new AddNotifications<Product>(this).IfNullOrEmpty(x => x.Name, "O nome é obrigatório.");
            new AddNotifications<Product>(this).IfNullOrInvalidLength(x => x.Name, 1, 50, "O nome deve conter de 1 á 50 caracteres.");
            new AddNotifications<Product>(this).IfEqualsZero(x => x.Price, "O preço não pode ser igual a zero.");
            new AddNotifications<Product>(this).IfNullOrEmpty(x => x.CategoryId, "A categoria é obrigatório.");
        }

    }
}
