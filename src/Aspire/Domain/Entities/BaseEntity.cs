using System;
using System.Diagnostics.CodeAnalysis;

using Aspire.Json;

namespace Aspire.Domain.Entities
{
    public abstract class BaseEntity : BaseEntity<long>
    {
    }

    public abstract class BaseEntity<TId> : IBaseEntity
    {
        public abstract TId Id { get; set; }
        public abstract DateTime CreatedAt { get; set; }
        public abstract DateTime UpdatedAt { get; set; }
        public abstract bool IsDeleted { get; set; }
        public abstract DateTime DeletedAt { get; set; }

        public static bool operator ==([NotNull] BaseEntity<TId> a, [NotNull] BaseEntity<TId> b)
        {
            return a.Id.Equals(b.Id);
        }

        public static bool operator !=([NotNull] BaseEntity<TId> a, [NotNull] BaseEntity<TId> b)
        {
            return !(a == b);
        }

        public override bool Equals([NotNull] object obj)
        {
            if (obj is BaseEntity<TId> baseEntity)
            {
                return baseEntity == this;
            }

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString()
        {
            return this.Serialize();
        }
    }

    public interface IBaseEntity
    {
        public abstract DateTime CreatedAt { get; set; }

        public abstract DateTime UpdatedAt { get; set; }

        public abstract bool IsDeleted { get; set; }

        public abstract DateTime DeletedAt { get; set; }
    }
}
