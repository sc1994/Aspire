using Autofac;
using Template.Application.Share;
using Template.Core.Share;

namespace Template.Application.BizA;

public class DemoApplication : Application<DemoPo, Guid, DemoDto, DemoDto, DemoDto>, IDemoApplication
{
    public string GetName()
    {
        return "BizA";
    }

    public DemoApplication(IComponentContext iocContext) : base(iocContext)
    {
    }
}