using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Domain.Entities
{
    public abstract class BaseEfCoreEntity : BaseEfCoreEntity<long>
    {

    }

    public abstract class BaseEfCoreEntity<TId> : BaseEntity<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override TId Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override DateTime CreatedAt { get; set; } = DateTime.Now;

        public override DateTime UpdatedAt { get; set; } = DateTime.MaxValue;

        public override bool IsDeleted { get; set; } = false;

        public override DateTime DeletedAt { get; set; } = DateTime.MaxValue;
    }
}
