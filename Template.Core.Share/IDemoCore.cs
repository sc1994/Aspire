using Template.Entity;

namespace Template.Core.Share;

public interface IDemo : ICore<Demo, Guid, DemoPo, DemoPo, DemoPo>
{
    IEnumerable<DemoPo> GetHello();
}