using Aspire;

using FreeSql;

using ServicesAndApplications.Entity;

using System;

namespace ServicesAndApplications.Applications
{
    public class CrudApplication : ApplicationCrudFreeSql<Table1, Guid, Table1, Table1, Table1Dto>
    {
        public CrudApplication(IRepository<Table1> repository, IAspireMapper aspireMapper) : base(repository, aspireMapper)
        {
        }

        protected override ISelect<Table1> QueryFilter(Table1 queryParam)
        {
            throw new System.NotImplementedException();
        }
    }
}
