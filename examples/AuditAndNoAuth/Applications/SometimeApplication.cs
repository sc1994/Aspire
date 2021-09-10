using Aspire;

using AuditAndNoAuth.Domains;

using FreeSql;

namespace AuditAndNoAuth.Applications;

public class SometimeApplication : ApplicationCrudFreeSql<Something>
{
    private readonly IRepositoryFreeSql<Something> repository;

    public SometimeApplication(IRepositoryFreeSql<Something> repository, IAspireMapper aspireMapper)
        : base(repository, aspireMapper)
    {
        this.repository = repository;
    }

    protected override ISelect<Something> QueryFilter(Something queryParam)
    {
        return repository.Select();
    }
}
