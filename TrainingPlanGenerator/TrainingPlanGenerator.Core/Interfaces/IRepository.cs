using System.Linq.Expressions;
using TrainingPlanGenerator.Core.ProjectAggregate;

namespace TrainingPlanGenerator.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetSingle(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes);

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);

        void SaveChanges();
    }
}
