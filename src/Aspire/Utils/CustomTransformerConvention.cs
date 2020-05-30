using Microsoft.AspNetCore.Routing;

namespace Aspire.Utils
{
    internal class CustomTransformerConvention : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            var route = value?.ToString();
            if (string.IsNullOrWhiteSpace(route)) return route;

            route = route.Replace("AppService", "");
            return ConvertHelper.ToSlugifyNamingStyle(route);
        }
    }
}