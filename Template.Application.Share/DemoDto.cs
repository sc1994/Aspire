using Template.Util;

namespace Template.Application.Share;

public class DemoDto : IPrimaryKey<Guid>
{
    public Guid Id { get; set; }
}