using System.Linq.Expressions;
using Autofac;
using AutoMapper;
using FreeSql;
using Template.Core.Share;
using Template.Entity;
using Template.Util;

namespace Template.Core;

public abstract class Core<TEntity, TPo, TPrimaryKey> : ICore<TPo, TPrimaryKey>, IFreeSqlExtensions<TEntity, TPrimaryKey>
    where TEntity : class, IPrimaryKey<TPrimaryKey>
    where TPo : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    private readonly IRepository<TEntity, TPrimaryKey> _repository;
    private readonly IMapper _mapper;

    protected Core(IComponentContext iocContext)
    {
        _repository = iocContext.Resolve<IRepository<TEntity, TPrimaryKey>>();
        _mapper = iocContext.Resolve<IMapper>();
    }

    public async Task<TPo> CreateAsync(TPo input)
    {
        var outs = await CreateBatchAsync(new[] {input});

        return outs.FirstOrDefault() ?? throw new Exception("创建失败");
    }

    public async Task<bool> DeleteAsync(TPrimaryKey id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<TPo> UpdateAsync(TPo input)
    {
        var outs = await UpdateBatchAsync(new[] {input});

        return outs.FirstOrDefault() ?? throw new Exception("更新失败");
    }

    public async Task<TPo?> GetAsync(TPrimaryKey id)
    {
        var outs = await GetListAsync(new[] {id});

        return outs.FirstOrDefault();
    }

    public async Task<IEnumerable<TPo>> CreateBatchAsync(IEnumerable<TPo> inputs)
    {
        if (!inputs.Any()) return Array.Empty<TPo>();

        var inputEntities = ToEntities(inputs);
        var outputEntities = await _repository.CreateBatchAsync(inputEntities);

        return ToPos(outputEntities);
    }

    public async Task<int> DeleteBatchAsync(IEnumerable<TPrimaryKey> ids)
    {
        if (!ids.Any()) return 0;

        return await _repository.DeleteBatchAsync(ids);
    }

    public async Task<IEnumerable<TPo>> UpdateBatchAsync(IEnumerable<TPo> inputs)
    {
        if (!inputs.Any()) return Array.Empty<TPo>();

        var existEntities = await _repository.GetListAsync(inputs.Select(x => x.Id));
        var inputEntities = ToEntities(existEntities.Select(x => (inputs.First(f => f.Id.Equals(x.Id)), x)));
        var outputEntities = await _repository.UpdateBatchAsync(inputEntities);

        return ToPos(outputEntities);
    }

    public async Task<IEnumerable<TPo>> GetListAsync(IEnumerable<TPrimaryKey> ids)
    {
        if (!ids.Any()) return Array.Empty<TPo>();

        var entities = await _repository.GetListAsync(ids);

        return ToPos(entities);
    }

    public async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> exp)
    {
        return await _repository.DeleteBatchAsync(exp);
    }

    public async Task<int> UpdateBatchAsync(Action<IUpdate<TEntity>> updateExp, Expression<Func<TEntity, bool>> exp)
    {
        return await _repository.UpdateBatchAsync(updateExp, exp);
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> exp, string orderBy = "", int limit = 0, int skip = 0)
    {
        return await _repository.GetListAsync(exp, orderBy, limit, skip);
    }

    protected TEntity ToEntity(TPo po)
    {
        return _mapper.Map<TEntity>(po);
    }

    protected IEnumerable<TEntity> ToEntities(IEnumerable<TPo> pos)
    {
        return _mapper.Map<IEnumerable<TEntity>>(pos);
    }

    protected TEntity ToEntity(TPo po, TEntity existing)
    {
        return _mapper.Map(po, existing);
    }

    protected IEnumerable<TEntity> ToEntities(IEnumerable<(TPo update, TEntity existing)> inputs)
    {
        foreach (var item in inputs)
        {
            yield return _mapper.Map(item.update, item.existing);
        }
    }

    protected TPo ToPo(TEntity entity)
    {
        return _mapper.Map<TPo>(entity);
    }

    protected IEnumerable<TPo> ToPos(IEnumerable<TEntity> entities)
    {
        return _mapper.Map<IEnumerable<TPo>>(entities);
    }
}