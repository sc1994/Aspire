using Template.Util;

namespace Template.Application.Domain;

public class DemoVO : IPrimaryKey<Guid>
{
    public Guid Id { get; set; }
}