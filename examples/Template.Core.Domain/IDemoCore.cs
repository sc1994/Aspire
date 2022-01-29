using Aspire.Core.Domain;

namespace Template.Core;

public interface IDemoCore : ICore<DemoDto, Guid, DemoDto>
{
    string GetHello();
}