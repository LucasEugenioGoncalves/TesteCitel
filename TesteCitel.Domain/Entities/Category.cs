using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using TesteCitel.Domain.Entities.Base;

namespace TesteCitel.Domain.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public Category(string name)
        {
            Name = name;

            new AddNotifications<Category>(this).IfNullOrEmpty(x => x.Name, "O nome é obrigatório.");
            new AddNotifications<Category>(this).IfNullOrInvalidLength(x => x.Name, 1, 50, "O nome deve conter de 1 á 50 caracteres.");
        }

        public string Name { get; private set; }
        public virtual ICollection<Product> Products { get; set; }
        public void Update(string name)
        {
            Name = name;

            new AddNotifications<Category>(this).IfNullOrEmpty(x => x.Name, "O nome é obrigatório.");
            new AddNotifications<Category>(this).IfNullOrInvalidLength(x => x.Name, 1, 50, "O nome deve conter de 1 á 50 caracteres.");
        }
    }
}
