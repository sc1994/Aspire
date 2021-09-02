using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     包含了构建aspire扩展程序所需要的内容.
    /// </summary>
    public interface IAspireBuilder
    {
        /// <summary>
        ///     Gets IMvcBuilder.
        /// </summary>
        IMvcBuilder MvcBuilder { get; }

        /// <summary>
        ///     Gets IServiceCollection.
        /// </summary>
        IServiceCollection ServiceCollection { get; }
    }

    /// <inheritdoc />
    public class AspireBuilder : IAspireBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AspireBuilder" /> class.
        /// </summary>
        /// <param name="mvcBuilder">IMvcBuilder.</param>
        /// <param name="serviceCollection">IServiceCollection.</param>
        public AspireBuilder(
            IMvcBuilder mvcBuilder,
            IServiceCollection serviceCollection)
        {
            MvcBuilder = mvcBuilder;
            ServiceCollection = serviceCollection;
        }

        /// <inheritdoc />
        public IMvcBuilder MvcBuilder { get; }

        /// <inheritdoc />
        public IServiceCollection ServiceCollection { get; }
    }
}