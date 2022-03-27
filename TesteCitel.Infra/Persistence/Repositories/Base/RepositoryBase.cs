using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TesteCitel.Domain.Entities.Base;
using TesteCitel.Domain.Interfaces.Repositories.Base;

namespace TesteCitel.Infra.Persistence.Repositories.Base
{
    public class RepositoryBase<TEntidade> : IRepositoryBase<TEntidade>
        where TEntidade : EntityBase
    {
        private readonly bd_citelContext _context;
        public RepositoryBase(bd_citelContext context)
        {
            _context = context;

        }
        public IQueryable<TEntidade> GetAllBy(bool asNoTracking = true, Expression<Func<TEntidade, bool>> where = null, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null)
        {
            if (where == null) return GetAll(asNoTracking, includeProperties);

            return GetAll(asNoTracking, includeProperties).Where(where);
        }
        public IQueryable<TEntidade> GetAllAndOrderBy<TKey>(bool asNoTracking = true, Expression<Func<TEntidade, bool>> where = null, Expression<Func<TEntidade, TKey>> ordem = null, bool ascendente = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null)
        {
            return ascendente ? GetAllBy(asNoTracking, where, includeProperties).OrderBy(ordem) : GetAllBy(asNoTracking, where, includeProperties).OrderByDescending(ordem);
        }
        public async Task<TEntidade> GetByAsync(bool asNoTracking = true, Expression<Func<TEntidade, bool>> where = null, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAll(asNoTracking, includeProperties).FirstOrDefaultAsync(where, cancellationToken);
        }
        public async Task<TEntidade> GetByIdAsync(string id, bool asNoTracking = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null)
        {
            if (includeProperties != null)
            {
                try
                {
                    return await GetAll(asNoTracking, includeProperties).FirstOrDefaultAsync(x => x.Id == id);
                }
                catch (Exception)
                {
                    throw;
                }

            }

            return await _context.Set<TEntidade>().FindAsync(id);
        }
        public IQueryable<TEntidade> GetAll(bool asNoTracking = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null)
        {
            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            if (asNoTracking)
            {
                return query.AsNoTracking();
            }

            return query;
        }
        public IQueryable<TEntidade> GetAllOrderBy<TKey>(bool asNoTracking = true, Expression<Func<TEntidade, TKey>> ordem = null, bool ascendente = true, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null)
        {
            return ascendente ? GetAll(asNoTracking, includeProperties).OrderBy(ordem) : GetAll(asNoTracking, includeProperties).OrderByDescending(ordem);
        }
        public async Task InsertAsync(TEntidade entidade)
        {
            await _context.Set<TEntidade>().AddAsync(entidade);
        }
        public async Task ExecuteSqlCommandAsync(string sql)
        {
            await _context.Database.ExecuteSqlRawAsync(sql);
        }
        public TEntidade Update(TEntidade entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            return entidade;
        }
        public void Remove(TEntidade entidade)
        {
            _context.Set<TEntidade>().Remove(entidade);
        }
        public async Task InsertListAsync(IEnumerable<TEntidade> entidades)
        {
            await _context.Set<TEntidade>().AddRangeAsync(entidades);
        }
        public async Task<bool> ExistAsync(Expression<Func<TEntidade, bool>> where, Func<IQueryable<TEntidade>, IIncludableQueryable<TEntidade, object>> includeProperties = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntidade> query = _context.Set<TEntidade>();

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            return await query.AnyAsync(where, cancellationToken);
        }
        public void RemoveList(IEnumerable<TEntidade> entidades)
        {
            _context.Set<TEntidade>().RemoveRange(entidades);
        }
        private IQueryable<TEntidade> Include(IQueryable<TEntidade> query, params Expression<Func<TEntidade, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            return query;
        }
    }
}
