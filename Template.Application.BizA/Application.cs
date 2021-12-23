using Autofac;
using AutoMapper;
using Template.Application.Share;
using Template.Core.Share;
using Template.Util;

namespace Template.Application.BizA;

public abstract class Application<TPo, TPrimaryKey, TOutput, TCreate, TUpdate> : IApplication<TPrimaryKey, TOutput, TCreate, TUpdate>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TPo : IPrimaryKey<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
    where TUpdate : IPrimaryKey<TPrimaryKey>
{
    private readonly ICore<TPo, TPrimaryKey> _core;
    private readonly IMapper _mapper;

    public Application(IComponentContext iocContext)
    {
        _core = iocContext.Resolve<ICore<TPo, TPrimaryKey>>();
        _mapper = iocContext.Resolve<IMapper>();
    }

    public async Task<TOutput> CreateAsync(TCreate input)
    {
        var po = _mapper.Map<TPo>(input);
        var result = await _core.CreateAsync(po);
        return _mapper.Map<TOutput>(result);
    }

    public async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        return await _core.DeleteAsync(id);
    }

    public async Task<TOutput> UpdateAsync(TUpdate input)
    {
        var po = _mapper.Map<TPo>(input);
        var result = await _core.UpdateAsync(po);
        return _mapper.Map<TOutput>(result);
    }

    public async Task<TOutput?> GetAsync(TPrimaryKey id)
    {
        var result = await _core.GetAsync(id);
        return result is null ? default : _mapper.Map<TOutput>(result);
    }

    protected TPo ToPo(TCreate create)
    {
        return _mapper.Map<TPo>(create);
    }

    protected IEnumerable<TPo> ToPos(IEnumerable<TCreate> creates)
    {
        return _mapper.Map<IEnumerable<TPo>>(creates);
    }

    protected TPo ToPo(TUpdate po, TPo existing)
    {
        return _mapper.Map(po, existing);
    }

    protected IEnumerable<TPo> ToEntities(IEnumerable<(TPo update, TPo existing)> inputs)
    {
        foreach (var item in inputs)
        {
            yield return _mapper.Map(item.update, item.existing);
        }
    }

    protected TOutput ToOutput(TPo po)
    {
        return _mapper.Map<TOutput>(po);
    }

    protected IEnumerable<TOutput> ToOutputs(IEnumerable<TPo> pos)
    {
        return _mapper.Map<IEnumerable<TOutput>>(pos);
    }
}