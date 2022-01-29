using TrainingPlanGenerator.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainingPlanGenerator.Core.ProjectAggregate;

namespace TrainingPlanGenerator.Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(criteria);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefault();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(criteria);
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(criteria);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(criteria);
            return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
