using System;

namespace Aspire
{
    /// <summary>
    /// 日志追踪值.
    /// </summary>
    public interface IlogTracer
    {
        /// <summary>
        /// Gets 追踪Id.
        /// </summary>
        string TraceId { get; }
    }

    /// <inheritdoc />
    public class DefaultLogTracer : IlogTracer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogTracer"/> class.
        /// 通过 di scope 注入.
        /// </summary>
        public DefaultLogTracer()
        {
            TraceId = Guid.NewGuid().ToString();
        }

        /// <inheritdoc />
        public string TraceId { get; }
    }
}