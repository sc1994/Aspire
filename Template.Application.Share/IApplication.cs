using Template.Util;

namespace Template.Application.Share;

public interface IApplication<in TPrimaryKey, TOutput, in TCreate, in TUpdate>
    : ICrud<TPrimaryKey, TOutput, TCreate, TUpdate>
    where TPrimaryKey : IEquatable<TPrimaryKey>
    where TOutput : IPrimaryKey<TPrimaryKey>
    where TUpdate : IPrimaryKey<TPrimaryKey>
{
    
}