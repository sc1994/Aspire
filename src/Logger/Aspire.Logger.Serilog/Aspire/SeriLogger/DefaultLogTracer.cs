using System;
using System.Threading;
using Aspire.Helpers;

namespace Aspire.SeriLogger
{
    /// <inheritdoc />
    public class DefaultLogTracer : ILogTracer
    {
        private static readonly AsyncLocal<string> AsyncLocal = new ();

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultLogTracer" /> class.
        ///     通过 di scope 注入.
        /// </summary>
        public DefaultLogTracer()
        {
            TraceId = AsyncLocal.Value ??= Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now.ToTimestamp();
        }

        /// <inheritdoc />
        public string TraceId { get; }

        /// <inheritdoc />
        public long CreatedAt { get; }
    }
}