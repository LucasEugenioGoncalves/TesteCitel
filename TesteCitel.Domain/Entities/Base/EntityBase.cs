using prmToolkit.NotificationPattern;
using System;
using System.ComponentModel.DataAnnotations;

namespace TesteCitel.Domain.Entities.Base
{
    public abstract class EntityBase : Notifiable
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; protected set; }
    }
}
