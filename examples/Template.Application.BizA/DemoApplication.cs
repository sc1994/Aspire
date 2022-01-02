using Aspire.Application;
using Autofac;
using Template.Core;

namespace Template.Application.BizA;

public class DemoApplication : BaseApplication<DemoDto, Guid, DemoVo, DemoVo, DemoVo, DemoVo>, IDemoApplication
{
    public DemoApplication(IComponentContext iocContext) : base(iocContext)
    {
    }

    public string GetName()
    {
        return "BizA";
    }
}