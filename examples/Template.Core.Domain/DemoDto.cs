using Template.Util;

namespace Template.Core;

public class DemoDto : IPrimaryKey<Guid>
{
    public Guid Id { get; set; }
}