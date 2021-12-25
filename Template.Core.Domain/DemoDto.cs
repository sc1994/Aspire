using Template.Util;

namespace Template.Core.Share;

public class DemoDto : IPrimaryKey<Guid>
{
    public Guid Id { get; set; }
}