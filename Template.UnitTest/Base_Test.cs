using System;
using Autofac;
using Moq;
using Template.Util;
using Xunit.Abstractions;

namespace Template.UnitTest;

public abstract class Base_Test
{
    protected readonly ITestOutputHelper Output;
    protected readonly IContainer Container;

    protected Base_Test(ITestOutputHelper output, Action<ContainerBuilder> actionBuilder)
    {
        Output = output;
        var builder = new ContainerBuilder();

        builder.Register(cxt => new CurrentUser_Test
        {
            UserName = "util test user"
        }).As<ICurrentUser>();

        actionBuilder(builder);
        Container = builder.Build();
    }
}

public class CurrentUser_Test : ICurrentUser
{
    public string UserName { get; set; } = string.Empty;
}