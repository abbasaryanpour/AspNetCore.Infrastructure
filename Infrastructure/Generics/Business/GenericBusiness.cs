namespace Infrastructure.Generics.Business;

public abstract class GenericBusiness<TRepository, TEntity> : IGenericBusiness<TRepository, TEntity> where TRepository : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly TRepository Repository;
    protected GenericBusiness(TRepository repository)
    {
        Repository = repository;
    }

    public Task<ActionReport> Delete(TEntity entity)
        => Repository.Delete(entity);

    public Task<ActionReport> Delete(IEnumerable<TEntity> entities) => Repository.Delete(entities);

    public Task<ActionReport> Delete(int id)
        => Repository.Delete(id);

    public Task<List<TEntity>> GetAll()
        => Repository.GetAll().ToListAsync();

    public Task<List<TEntity>> GetAll_Ignored() => Repository.GetAll_Ignored().ToListAsync();

    public Task<List<TEntity>> GetByConditions(Expression<Func<TEntity, bool>> expression, bool isTracking = false)
        => Repository.GetByConditions(expression, isTracking).ToListAsync();

    public Task<List<TEntity>> GetByConditions_Ignored(Expression<Func<TEntity, bool>> expression, bool isTracking = false)
        => Repository.GetByConditions_Ignored(expression, isTracking).ToListAsync();

    public Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false)
        => Repository.FirstOrDefault(expression, isTracking);

    public Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false)
        => Repository.SingleOrDefault(expression, isTracking);

    public Task<TEntity> GetById(int id, bool isTracking = false)
        => Repository.GetById(id, isTracking);

    public Task<ActionReport> Insert(TEntity entity)
        => Repository.Insert(entity);

    public Task<ActionReport> Insert(IEnumerable<TEntity> entities)
        => Repository.Insert(entities);

    public Task<ActionReport> Update(TEntity entity)
        => Repository.Update(entity);
    public Task<ActionReport> Update(IEnumerable<TEntity> entity)
        => Repository.Update(entity);

    public Task<ActionReport> UpdateFields(TEntity entity, List<string> fields)
        => Repository.UpdateFields(entity, fields);
}
