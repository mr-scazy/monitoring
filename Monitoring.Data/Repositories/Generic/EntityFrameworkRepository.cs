using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitoring.Data.EntityFramework;
using Monitoring.Domain.Interfaces;

namespace Monitoring.Data.Repositories.Generic
{

    public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EntityFrameworkRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll() 
            => _dbSet;

        public T GetById(object id)
            => _dbSet.Find(id);

        public Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
            => _dbSet.FindAsync(new [] {id}, cancellationToken);

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<object> ids)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
