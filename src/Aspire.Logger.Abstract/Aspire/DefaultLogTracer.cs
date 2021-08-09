using System;

namespace Aspire
{
    /// <inheritdoc />
    public class DefaultLogTracer : IlogTracer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultLogTracer" /> class.
        ///     通过 di scope 注入.
        /// </summary>
        public DefaultLogTracer()
        {
            TraceId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }

        /// <inheritdoc />
        public string TraceId { get; }

        /// <inheritdoc />
        public DateTime CreatedAt { get; }
    }
}