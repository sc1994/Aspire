using Aspire.Entity;
using FreeSql.DataAnnotations;

namespace Aspire.FreeSql.Entity;

public abstract class FullEntity<TPrimaryKey> : IPrimaryKey<TPrimaryKey>, ICreated, IUpdated, IDeleted
    where TPrimaryKey : struct, IEquatable<TPrimaryKey>
{
    [Column(CanUpdate = false)] public DateTime CreatedAt { get; set; } = Const.DefaultDateTime;

    [Column(CanUpdate = false)] public string CreatedBy { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public DateTime DeletedAt { get; set; } = Const.DefaultDateTime;

    public string DeletedBy { get; set; } = string.Empty;

    [Column(IsIdentity = true, IsPrimary = true, CanUpdate = false)]
    public TPrimaryKey Id { get; set; }

    public DateTime UpdatedAt { get; set; } = Const.DefaultDateTime;

    public string UpdatedBy { get; set; } = string.Empty;
}