using System;

namespace Aspire
{
    /// <summary>
    ///     日志追踪值.
    /// </summary>
    public interface IlogTracer
    {
        /// <summary>
        ///     Gets 追踪Id.
        /// </summary>
        string TraceId { get; }

        /// <summary>
        ///     Gets 创建时间.
        /// </summary>
        DateTime CreatedAt { get; }
    }
}