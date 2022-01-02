using Aspire.Core.Domain;

namespace Template.Core;

public interface IDemoCore : ICore<DemoDto, Guid, DemoDto>
{
    IEnumerable<DemoDto> GetHello();
}