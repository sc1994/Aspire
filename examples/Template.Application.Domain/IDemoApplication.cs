using Aspire.Application.Domain;

namespace Template.Application;

public interface IDemoApplication : IApplication<Guid, DemoVo, DemoVo, DemoVo, DemoVo>
{
    string GetName();
}