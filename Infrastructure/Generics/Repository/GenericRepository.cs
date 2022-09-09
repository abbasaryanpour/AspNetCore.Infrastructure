namespace Infrastructure.Generics.Repository;

public abstract class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected DbContext DbContext;
    protected DbContext NoTrackingDbContext;
    protected DbSet<TEntity> Entities;
    protected IQueryable<TEntity> NoTrackingEntities;

    protected GenericRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        NoTrackingDbContext = DbContext;

        Entities = DbContext.Set<TEntity>();
        NoTrackingEntities = DbContext.Set<TEntity>().AsNoTracking();
    }

    public async Task<ActionReport> Delete(TEntity entity)
    {
        try
        {
            if (entity == null) return ActionReport.Error();
            Entities.Remove(entity);
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public async Task<ActionReport> Delete(IEnumerable<TEntity> entities)
    {
        try
        {
            if (entities == null) return ActionReport.Error();
            Entities.RemoveRange(entities);
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public async Task<ActionReport> Delete(int id)
    {
        try
        {
            TEntity? entity = await Entities.FindAsync(id);
            if (entity == null) return ActionReport.Error();
            Entities.Remove(entity);
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public void Dispose()
    {
        if (DbContext != null) DbContext.Dispose();
        if (NoTrackingDbContext != null) NoTrackingDbContext.Dispose();
    }

    public IQueryable<TEntity> GetAll() => NoTrackingEntities;

    public IQueryable<TEntity> GetAll_Ignored() => NoTrackingEntities.IgnoreQueryFilters();

    public IQueryable<TEntity> GetByConditions(Expression<Func<TEntity, bool>> expression, bool isTracking)
        => isTracking ? Entities.Where(expression) : NoTrackingEntities.Where(expression);

    public IQueryable<TEntity> GetByConditions_Ignored(Expression<Func<TEntity, bool>> expression, bool isTracking)
            => isTracking ? Entities.Where(expression).IgnoreQueryFilters() : NoTrackingEntities.IgnoreQueryFilters().Where(expression);

    public Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false)
=> isTracking ? Entities.FirstOrDefaultAsync(expression) : NoTrackingEntities.FirstOrDefaultAsync(expression);

    public Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = false)
        => isTracking ? Entities.SingleOrDefaultAsync(expression) : NoTrackingEntities.SingleOrDefaultAsync(expression);

    public Task<TEntity?> GetById(int id, bool isTracking)
        => isTracking ? Entities.SingleOrDefaultAsync(entity => entity.Id == id)
            : NoTrackingEntities.SingleOrDefaultAsync(entity => entity.Id == id);

    public async Task<ActionReport> Insert(TEntity entity)
    {
        try
        {
            if (entity == null) return ActionReport.Error();
            await Entities.AddAsync(entity);
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public async Task<ActionReport> Insert(IEnumerable<TEntity> entities)
    {
        try
        {
            if (!entities.Any()) return ActionReport.Error();
            await Entities.AddRangeAsync(entities);
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public async Task<ActionReport> Update(TEntity entity)
    {
        try
        {
            if (entity == null) return ActionReport.Error();
            Entities.Update(entity);
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public async Task<ActionReport> Update(IEnumerable<TEntity> entities)
    {
        try
        {
            if (!entities.Any())
                return ActionReport.Error();
            Entities.UpdateRange(entities);
            return await SaveChanges(false);
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    public async Task<ActionReport> UpdateFields(TEntity entity, List<string> fields)
    {
        try
        {
            Entities.Attach(entity);
            foreach (string property in fields)
                DbContext.Entry(entity).Property(property).IsModified = true;
            return await SaveChanges();
        }
        catch (Exception exception)
        {
            return ActionReport.Error(ReportErrorCode.InternalServerError, exception);
        }
    }

    private async Task<ActionReport> SaveChanges(bool checkResult = true)
    {
        int result = await DbContext.SaveChangesAsync();
        if (checkResult && result < 1)
            return ActionReport.Error();
        return ActionReport.Success();
    }
}
