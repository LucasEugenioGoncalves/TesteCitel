using System;
using TesteCitel.Domain.Entities;
using TesteCitel.Domain.Interfaces.Repositories;
using TesteCitel.Infra.Persistence.Repositories.Base;

namespace TesteCitel.Infra.Persistence.Repositories
{
    public class RepositoryProduct : RepositoryBase<Product>, IRepositoryProduct
    {
        protected readonly bd_citelContext _context;
        public RepositoryProduct(bd_citelContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
