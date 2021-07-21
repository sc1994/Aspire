using System;
using Aspire.Entity.Audit;
using FreeSql.DataAnnotations;

namespace Aspire.Entity
{
    /// <summary>
    ///     free sql 审计实体. 默认涵盖了 free sql 审计的字段实现.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    public class EntityAuditFreeSql<TPrimaryKey> :
        IEntityBase<TPrimaryKey>,
        IAuditSoftDelete,
        IAuditCreate,
        IAuditUpdate
    {
        /// <summary>
        ///     账户 字段类型. 表中需要冗余 账户的 使用此字段赋值DbType.
        /// </summary>
        protected const string AccountDbType = "varchar(64) NOT NULL";

        /// <inheritdoc />
        [Column(DbType = AccountDbType)]
        public string Creator { get; set; }

        /// <inheritdoc />
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        public bool Deleted { get; set; }

        /// <inheritdoc />
        public DateTime DeletedAt { get; set; }

        /// <inheritdoc />
        [Column(DbType = AccountDbType)]
        public string Deleter { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedAt { get; set; }

        /// <inheritdoc />
        [Column(DbType = AccountDbType)]
        public string Updater { get; set; }

        /// <inheritdoc />
        [Column(IsPrimary = true, IsIdentity = true)]
        public TPrimaryKey Id { get; set; }
    }
}