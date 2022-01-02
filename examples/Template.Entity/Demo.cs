using Aspire.FreeSql.Entity;

namespace Template.Entity;

public class Demo : FullEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
}