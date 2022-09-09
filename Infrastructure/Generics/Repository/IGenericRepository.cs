namespace Infrastructure.Generics.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAll_Ignored();
    IQueryable<TEntity> GetByConditions(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    IQueryable<TEntity> GetByConditions_Ignored(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<TEntity?> GetById(int id, bool isTracking = false);
    Task<ActionReport> Insert(TEntity entity);
    Task<ActionReport> Insert(IEnumerable<TEntity> entities);
    Task<ActionReport> Update(TEntity entity);
    Task<ActionReport> Update(IEnumerable<TEntity> entity);
    Task<ActionReport> UpdateFields(TEntity entity, List<string> fields);
    Task<ActionReport> Delete(TEntity entity);
    Task<ActionReport> Delete(IEnumerable<TEntity> entities);
    Task<ActionReport> Delete(int id);
}
