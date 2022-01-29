using Aspire.Core;
using Aspire.Core.Domain;
using Aspire.FreeSql.Extension;
using Autofac;
using Template.Entity;

namespace Template.Core;

public class DemoCoreCore : BaseCore<Demo, DemoDto, Guid, DemoDto>, IDemoCore
{
    public DemoCoreCore(IComponentContext iocContext) : base(iocContext)
    {
    }

    public string GetHello()
    {
        return "Hello " + DateTime.Now;
    }

    public override async Task<PageOutDto<DemoDto>> PagingAsync(int index, int size, DemoDto input)
    {
        var (total, list) = await Repository.Select().PagingAsync<Demo, Guid>(index, size);

        return new PageOutDto<DemoDto>(total, Mapper.Map<IEnumerable<DemoDto>>(list));
    }
}