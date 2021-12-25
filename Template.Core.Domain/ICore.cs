using Template.Util;

namespace Template.Core.Share;

public interface ICore<TPo, in TPrimaryKey>
    : ICrud<TPrimaryKey, TPo, TPo, TPo>, ICrudBatch<TPrimaryKey, TPo, TPo, TPo>
    where TPo : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
}