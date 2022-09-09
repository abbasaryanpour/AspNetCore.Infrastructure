using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Infrastructure.Generics.Controller;

public abstract class GenericController<TBusiness, TRepository, TEntity> : ControllerBase, IGenericController<TBusiness, TRepository, TEntity>
    where TEntity : BaseEntity
    where TRepository : IGenericRepository<TEntity>
    where TBusiness : IGenericBusiness<TRepository, TEntity>
{
    protected TBusiness Business;
    protected IHttpContextAccessor HttpContextAccessor;
    protected IMapper Mapper;
    protected IPAddress Ip => HttpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;

    protected GenericController(TBusiness business, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        Business = business;
        HttpContextAccessor = httpContextAccessor;
        Mapper = mapper;
    }

    protected ObjectResult InternalServerError(object? value = null)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, value);
    }

    protected OkObjectResult Ok<T>(object obj)
    {
        return Ok(Mapper.Map<T>(obj));
    }

    protected ObjectResult Status(ActionReport report, object? value = null)
    {
        return StatusCode((int)report.Code, value);
    }
}
