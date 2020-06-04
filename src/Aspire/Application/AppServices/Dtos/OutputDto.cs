using System;

namespace Aspire.Application.AppServices.Dtos
{
    public class OutputDto<TId> : InputDto<TId>
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
