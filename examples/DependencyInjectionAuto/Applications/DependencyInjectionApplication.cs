
using Aspire;

using DependencyInjectionAuto.Domains;

using System;

namespace DependencyInjectionAuto.Applications;

public class DependencyInjectionApplication : ApplicationBase
{
    private readonly IInterfaceDomain iInterfaceDomain;
    private readonly InterfaceDomain interfaceDomain;
    private readonly ScopedDomain scopedDomain;
    private readonly SingletonDomain singletonDomain;
    private readonly TransientDomain transientDomain;

    public DependencyInjectionApplication(IInterfaceDomain iInterfaceDomain, InterfaceDomain interfaceDomain, ScopedDomain scopedDomain, SingletonDomain singletonDomain, TransientDomain transientDomain)
    {
        this.iInterfaceDomain = iInterfaceDomain;
        this.interfaceDomain = interfaceDomain;
        this.scopedDomain = scopedDomain;
        this.singletonDomain = singletonDomain;
        this.transientDomain = transientDomain;
    }

    public string Get()
    {
        if (iInterfaceDomain == null
            || interfaceDomain == null
            || scopedDomain == null
            || singletonDomain == null
            || transientDomain == null)
        {
            throw new ArgumentNullException("参数注入失败");
        }

        if (iInterfaceDomain.GetType() != interfaceDomain.GetType())
        {
            throw new ArgumentNullException("参数注入错误");
        }

        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
