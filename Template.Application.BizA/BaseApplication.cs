using Autofac;
using AutoMapper;
using Template.Core;

namespace Template.Application.BizA;

public abstract class Application<TDto, TPrimaryKey, TPageParam, TOutput, TCreate, TUpdate>
    : IApplication<TPrimaryKey, TPageParam, TOutput, TCreate, TUpdate>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TDto : IPrimaryKey<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
    where TUpdate : IPrimaryKey<TPrimaryKey>
{
    private readonly ICore<TDto, TPrimaryKey> _core;
    private readonly IMapper _mapper;

    protected Application(IComponentContext iocContext)
    {
        _core = iocContext.Resolve<ICore<TDto, TPrimaryKey>>();
        _mapper = iocContext.Resolve<IMapper>();
    }

    public async Task<TOutput> CreateAsync(TCreate input)
    {
        var po = _mapper.Map<TDto>(input);
        var result = await _core.CreateAsync(po);
        return _mapper.Map<TOutput>(result);
    }

    public async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        return await _core.DeleteAsync(id);
    }

    public async Task<TOutput> UpdateAsync(TPrimaryKey primaryKey, TUpdate input)
    {
        var po = _mapper.Map<TDto>(input);
        var result = await _core.UpdateAsync(po);
        return _mapper.Map<TOutput>(result);
    }

    public async Task<TOutput?> GetAsync(TPrimaryKey id)
    {
        var result = await _core.GetAsync(id);
        return result is null ? default : _mapper.Map<TOutput>(result);
    }

    public Task<PageOut<TOutput>> Paging(int index, int size, TPageParam input)
    {
        throw new NotImplementedException();
    }

    protected TDto ToPo(TCreate create)
    {
        return _mapper.Map<TDto>(create);
    }

    protected IEnumerable<TDto> ToPos(IEnumerable<TCreate> creates)
    {
        return _mapper.Map<IEnumerable<TDto>>(creates);
    }

    protected TDto ToPo(TUpdate po, TDto existing)
    {
        return _mapper.Map(po, existing);
    }

    protected IEnumerable<TDto> ToEntities(IEnumerable<(TDto update, TDto existing)> inputs)
    {
        foreach (var item in inputs) yield return _mapper.Map(item.update, item.existing);
    }

    protected TOutput ToOutput(TDto po)
    {
        return _mapper.Map<TOutput>(po);
    }

    protected IEnumerable<TOutput> ToOutputs(IEnumerable<TDto> pos)
    {
        return _mapper.Map<IEnumerable<TOutput>>(pos);
    }
}