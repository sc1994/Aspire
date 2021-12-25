using System.ComponentModel.DataAnnotations;

namespace Aspire.Application;

public interface IApplication<in TPrimaryKey, in TPageParam, TOutput, in TCreate, in TUpdate>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
{
    Task<TOutput> CreateAsync([Required] TCreate input);

    Task<bool> DeleteAsync([Required] TPrimaryKey id);

    Task<TOutput> UpdateAsync([Required] TPrimaryKey primaryKey, [Required] TUpdate input);

    Task<TOutput?> GetAsync([Required] TPrimaryKey id);

    Task<PageOut<TOutput>> PagingAsync([Required] int index, [Required] int size, TPageParam input);
}