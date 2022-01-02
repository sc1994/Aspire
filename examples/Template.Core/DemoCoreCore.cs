using Aspire.Core;
using Aspire.Core.Domain;
using Autofac;
using Template.Entity;

namespace Template.Core;

public class DemoCoreCore : BaseCore<Demo, DemoDto, Guid, DemoDto>, IDemoCore
{
    public DemoCoreCore(IComponentContext iocContext) : base(iocContext)
    {
    }

    public IEnumerable<DemoDto> GetHello()
    {
        throw new NotImplementedException();
    }

    public override async Task<PageOutDto<DemoDto>> PagingAsync(int index, int size, DemoDto input)
    {
        throw new NotImplementedException();
    }
}