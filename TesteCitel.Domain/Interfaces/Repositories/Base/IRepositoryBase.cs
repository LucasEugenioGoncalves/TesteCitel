using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TesteCitel.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntidade>
       where TEntidade : class
    {
        Task<TEntidade> GetByAsync(bool asNoTracking = true, Expression<Func<TEntidade, bool>> where = null, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> ExistAsync(Expression<Func<TEntidade, bool>> where, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null, CancellationToken cancellationToken = default(CancellationToken));

        IQueryable<TEntidade> GetAllBy(bool asNoTracking = true, Expression<Func<TEntidade, bool>> where = null, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null);

        IQueryable<TEntidade> GetAll(bool asNoTracking = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null);

        IQueryable<TEntidade> GetAllOrderBy<TKey>(bool asNoTracking = true, Expression<Func<TEntidade, TKey>> ordem = null, bool ascendente = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null);

        Task<TEntidade> GetByIdAsync(string id, bool asNoTracking = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null);

        Task InsertAsync(TEntidade entidade);

        TEntidade Update(TEntidade entidade);

        void Remove(TEntidade entidade);

        Task InsertListAsync(IEnumerable<TEntidade> entidades);

        void RemoveList(IEnumerable<TEntidade> entidades);

        IQueryable<TEntidade> GetAllAndOrderBy<TKey>(bool asNoTracking = true, Expression<Func<TEntidade, bool>> where = null, Expression<Func<TEntidade, TKey>> ordem = null, bool ascendente = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null);

        Task ExecuteSqlCommandAsync(string sql);
    }
}
