using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;

namespace Aspire
{
    /// <summary>
    ///     app service base 继承此类, 将会将派生类中的公开方法暴露到 api 中.
    /// </summary>
    [DynamicWebApi]
    public abstract class AppServiceBase : IDynamicWebApi
    {
    }
}