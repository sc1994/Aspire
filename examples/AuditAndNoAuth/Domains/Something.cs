
using Aspire.Entity;

namespace AuditAndNoAuth.Domains;
public class Something : EntityFullAudit
{
    public string? Name { get; set; }
}
