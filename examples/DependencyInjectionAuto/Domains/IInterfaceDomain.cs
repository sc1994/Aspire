
using Aspire;

namespace DependencyInjectionAuto.Domains;

[InjectTo(Lifecycle.Scoped, typeof(InterfaceDomain))]
public interface IInterfaceDomain
{
}
