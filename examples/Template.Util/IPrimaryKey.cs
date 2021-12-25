namespace Template.Util;

public interface IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
}