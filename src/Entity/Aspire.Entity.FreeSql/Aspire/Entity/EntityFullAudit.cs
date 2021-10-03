using Aspire.Entity.Audit;

using FreeSql.DataAnnotations;

using System;

namespace Aspire.Entity
{
    /// <summary>
    ///     free sql 审计实体. 默认涵盖了 free sql 审计的字段实现.
    /// </summary>
    public abstract class EntityFullAudit : EntityFullAudit<Guid>, IEntityBase
    {
    }

    /// <summary>
    ///     free sql 审计实体. 默认涵盖了 free sql 审计的字段实现.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    public abstract class EntityFullAudit<TPrimaryKey> :
        IEntityBase<TPrimaryKey>,
        IAuditSoftDelete,
        IAuditCreate,
        IAuditUpdate
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     账户 字段类型. 表中需要冗余 账户的 使用此字段赋值DbType.
        /// </summary>
        protected const string AccountDbType = "varchar(64) NOT NULL";

        /// <inheritdoc />
        [Column(DbType = AccountDbType, CanUpdate = false)]
        public string Creator { get; set; } = string.Empty;

        /// <inheritdoc />
        [Column(CanUpdate = false)]
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        public bool Deleted { get; set; }

        /// <inheritdoc />
        public DateTime DeletedAt { get; set; }

        /// <inheritdoc />
        [Column(DbType = AccountDbType)]
        public string Deleter { get; set; } = string.Empty;

        /// <inheritdoc />
        public DateTime UpdatedAt { get; set; }

        /// <inheritdoc />
        [Column(DbType = AccountDbType)]
        public string Updater { get; set; } = string.Empty;

        /// <inheritdoc />
        [Column(IsPrimary = true, IsIdentity = true, CanUpdate = false, IsNullable = false)]
        public TPrimaryKey Id { get => id ?? throw new ArgumentNullException(nameof(Id)); set => id = value; }

        private TPrimaryKey? id;
    }

    /// <summary>
    ///     free sql 审计实体. 默认涵盖了 free sql 审计的字段实现.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    /// <typeparam name="TDatabase">指定当前实体所属的数据库.</typeparam>
    public class EntityFullAudit<TPrimaryKey, TDatabase> :
        EntityFullAudit<TPrimaryKey>,
        IEntityBase<TPrimaryKey, TDatabase>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}