using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Aspire;

public static class MiddlewareHelper
{
    public static (string, string) GetControllerAndActionName(this HttpContext httpContext)
    {
        var controllerActionDescriptor = httpContext
            .GetEndpoint()?
            .Metadata
            .GetMetadata<ControllerActionDescriptor>();

        if (controllerActionDescriptor == null) return;

        var controllerName = controllerActionDescriptor.ControllerTypeInfo.Name;
        var actionName = controllerActionDescriptor.MethodInfo.Name;
    }
}
