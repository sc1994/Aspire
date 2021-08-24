using Aspire;

namespace Aspire.SeriLogger
{
    /// <inheritdoc />
    public class DefaultLogTracer : ILogTracer
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultLogTracer" /> class.
        ///     通过 di scope 注入.
        /// </summary>
        public DefaultLogTracer()
        {
            TraceId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now.ToTimestamp();
        }

        /// <inheritdoc />
        public string TraceId { get; }

        /// <inheritdoc />
        public long CreatedAt { get; }
    }
}