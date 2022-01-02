using Aspire.Application.Domain;
using Aspire.Core.Domain;
using Autofac;
using AutoMapper;

namespace Aspire.Application;

public abstract class BaseApplication<TDto, TPrimaryKey, TPageParam, TOutput, TCreate, TUpdate> : IApplication<TPrimaryKey, TPageParam, TOutput, TCreate, TUpdate>
    where TDto : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
{
    protected readonly ICore<TDto, TPrimaryKey, TPageParam> Core;
    protected readonly IComponentContext IocContext;
    protected readonly Mapper Mapper;

    protected BaseApplication(IComponentContext iocContext)
    {
        IocContext = iocContext;

        Core = IocContext.Resolve<ICore<TDto, TPrimaryKey, TPageParam>>();
        Mapper = IocContext.Resolve<Mapper>();
    }

    public virtual async Task<TOutput> CreateAsync(TCreate input)
    {
        var dto = await Core.CreateAsync(ToDto(input));

        return ToOutput(dto);
    }

    public virtual async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        return await Core.DeleteAsync(id);
    }

    public virtual async Task<TOutput> UpdateAsync(TPrimaryKey primaryKey, TUpdate input)
    {
        var dto = ToDto(input);
        dto.Id = primaryKey;

        var updatedDto = await Core.UpdateAsync(dto);

        return ToOutput(updatedDto);
    }

    public virtual async Task<TOutput?> GetAsync(TPrimaryKey id)
    {
        var dto = await Core.GetAsync(id);

        return dto is null ? default : ToOutput(dto);
    }

    public virtual async Task<PageOutVm<TOutput>> PagingAsync(int index, int size, TPageParam input)
    {
        var res = await Core.PagingAsync(index, size, input);

        return ToPageOut(res);
    }

    protected TDto ToDto(TCreate create)
    {
        return Mapper.Map<TDto>(create);
    }

    protected TDto ToDto(TUpdate update)
    {
        return Mapper.Map<TDto>(update);
    }

    protected TOutput ToOutput(TDto dto)
    {
        return Mapper.Map<TOutput>(dto);
    }

    protected IEnumerable<TOutput> ToOutput(IEnumerable<TDto> dtoList)
    {
        if (!dtoList.Any()) return Array.Empty<TOutput>();
        return Mapper.Map<IEnumerable<TOutput>>(dtoList);
    }

    protected PageOutVm<TOutput> ToPageOut(PageOutDto<TDto> input)
    {
        return new PageOutVm<TOutput>(input.TotalCount, ToOutput(input.Items));
    }
}