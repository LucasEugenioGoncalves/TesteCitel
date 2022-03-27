using System;
using TesteCitel.Domain.Entities;
using TesteCitel.Domain.Interfaces.Repositories;
using TesteCitel.Infra.Persistence.Repositories.Base;

namespace TesteCitel.Infra.Persistence.Repositories
{
    public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
    {
        protected readonly bd_citelContext _context;
        public RepositoryCategory(bd_citelContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
