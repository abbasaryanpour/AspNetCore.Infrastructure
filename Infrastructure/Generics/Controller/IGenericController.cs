namespace Infrastructure.Generics.Controller;

public interface IGenericController<TBusiness, TRepository, TEntity>
    where TEntity : BaseEntity
    where TRepository : IGenericRepository<TEntity>
    where TBusiness : IGenericBusiness<TRepository, TEntity>
{

}
