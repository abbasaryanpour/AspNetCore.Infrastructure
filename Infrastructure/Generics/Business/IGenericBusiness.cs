namespace Infrastructure.Generics.Business;

public interface IGenericBusiness<TRepository, TEntity> where TRepository : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAll();
    Task<List<TEntity>> GetAll_Ignored();
    Task<List<TEntity>> GetByConditions(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<List<TEntity>> GetByConditions_Ignored(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false);
    Task<TEntity> GetById(int id, bool isTracking = false);
    Task<ActionReport> Insert(TEntity entity);
    Task<ActionReport> Insert(IEnumerable<TEntity> entities);
    Task<ActionReport> Update(TEntity entity);
    Task<ActionReport> Update(IEnumerable<TEntity> entity);
    Task<ActionReport> UpdateFields(TEntity entity, List<string> fields);
    Task<ActionReport> Delete(TEntity entity);
    Task<ActionReport> Delete(IEnumerable<TEntity> entities);
    Task<ActionReport> Delete(int id);
}
