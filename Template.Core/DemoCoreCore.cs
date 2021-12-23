using Autofac;
using Template.Core.Share;
using Template.Entity;

namespace Template.Core;

public class DemoCoreCore : Core<Demo, DemoPo, Guid>, IDemoCore
{
    public DemoCoreCore(IComponentContext iocContext) : base(iocContext)
    {
    }

    public IEnumerable<DemoPo> GetHello()
    {
        throw new NotImplementedException();
    }
}