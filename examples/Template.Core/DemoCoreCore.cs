using Autofac;
using Template.Entity;

namespace Template.Core;

public class DemoCoreCore : Core<Demo, DemoDto, Guid>, IDemoCore
{
    public DemoCoreCore(IComponentContext iocContext) : base(iocContext)
    {
    }

    public IEnumerable<DemoDto> GetHello()
    {
        throw new NotImplementedException();
    }
}