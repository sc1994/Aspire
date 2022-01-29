namespace Aspire.Repository;

public interface IUnitOfWork<in TEntity, TPrimaryKey>
    where TEntity : IPrimaryKey<TPrimaryKey>
    where TPrimaryKey : IEquatable<TPrimaryKey>
{
    Dictionary<string, object> Content { get; init; }

    TPrimaryKey Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TPrimaryKey primaryKey);

    int Commit();
    void Rollback();
}