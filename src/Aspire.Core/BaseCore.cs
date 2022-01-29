using Aspire.Core.Domain;
using Aspire.Repository;
using Autofac;
using AutoMapper;

namespace Aspire.Core;

public abstract class BaseCore<TEntity, TDto, TPrimaryKey, TPageParam> : ICore<TDto, TPrimaryKey, TPageParam>
    where TEntity : IPrimaryKey<TPrimaryKey>
    where TDto : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    protected readonly IComponentContext IocContext;
    protected readonly IMapper Mapper;
    protected readonly IRepository<TEntity, TPrimaryKey> Repository;

    protected BaseCore(IComponentContext iocContext)
    {
        IocContext = iocContext;
        Repository = IocContext.Resolve<IRepository<TEntity, TPrimaryKey>>();
        Mapper = IocContext.Resolve<IMapper>();
    }

    public virtual async Task<IEnumerable<TDto>> CreateBatchAsync(IEnumerable<TDto> inputs)
    {
        var entities = ToEntities(inputs);
        var ids = await Repository.CreateBatchAsync(entities);

        return await GetListAsync(ids);
    }

    public virtual async Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids)
    {
        return await Repository.DeleteBatchAsync(ids);
    }

    public virtual async Task<IEnumerable<TDto>> UpdateBatchAsync(IEnumerable<TDto> inputs)
    {
        var oldEntities = await Repository.GetListAsync(inputs.Select(x => x.Id));

        var relation = oldEntities.Select(x => (inputs.First(f => f.Id.Equals(x.Id)), x));
        var newEntities = ToEntities(relation);

        _ = await Repository.UpdateBatchAsync(newEntities);

        return await GetListAsync(inputs.Select(x => x.Id));
    }

    public virtual async Task<IEnumerable<TDto>> GetListAsync(IEnumerable<TPrimaryKey> ids)
    {
        var entities = await Repository.GetListAsync(ids);

        return Mapper.Map<IEnumerable<TDto>>(entities);
    }

    public abstract Task<PageOutDto<TDto>> PagingAsync(int index, int size, TPageParam input);

    protected virtual IEnumerable<TDto> ToDtos(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities) yield return ToDto(entity);
    }

    protected virtual TDto ToDto(TEntity entity)
    {
        return Mapper.Map<TDto>(entity);
    }

    protected virtual IEnumerable<TEntity> ToEntities(IEnumerable<TDto> dtos)
    {
        return dtos.Select(ToEntity);
    }

    protected virtual TEntity ToEntity(TDto dto)
    {
        return Mapper.Map<TEntity>(dto);
    }

    protected virtual IEnumerable<TEntity> ToEntities(IEnumerable<(TDto dto, TEntity entity)> inputs)
    {
        return inputs.Select(input => ToEntity(input.dto, input.entity));
    }

    protected virtual TEntity ToEntity(TDto dto, TEntity entity)
    {
        return Mapper.Map(dto, entity);
    }
}