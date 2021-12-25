using Template.Util;

namespace Template.Core;

public interface ICore<TDto, in TPrimaryKey> : ICrud<TPrimaryKey, TDto, TDto, TDto>, ICrudBatch<TPrimaryKey, TDto, TDto, TDto>
    where TDto : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
}