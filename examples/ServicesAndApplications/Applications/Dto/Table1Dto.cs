using Aspire;

using ServicesAndApplications.Entity;

using System;

namespace ServicesAndApplications.Applications
{
    [MapTo(typeof(Table1))]
    public class Table1Dto : IPrimaryKey<Guid>
    {
        public Guid Id { get; set; }
    }
}