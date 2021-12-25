namespace Aspire;

public interface IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
}