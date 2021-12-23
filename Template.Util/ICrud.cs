namespace Template.Util;

public interface ICrud<in TPrimaryKey, TOutput, in TCreate, in TUpdate>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
{
    Task<TOutput> CreateAsync(TCreate input);

    Task<bool> DeleteAsync(TPrimaryKey id);

    Task<TOutput> UpdateAsync(TUpdate input);

    Task<TOutput?> GetAsync(TPrimaryKey id);
}