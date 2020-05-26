using Aspire.Domain.Entities;

using System;

namespace Aspire.EfCore.Test
{
    public class EfCoreEntity_Long_Test : EfCoreEntity_Test<long>
    {

    }


    public class EfCoreEntity_Guid_Test : EfCoreEntity_Test<Guid>
    {

    }

    public class EfCoreEntity_Test<TId> : BaseEfCoreEntity<TId>
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}

