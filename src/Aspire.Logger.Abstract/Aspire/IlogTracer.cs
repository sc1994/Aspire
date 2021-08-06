namespace Aspire
{
    /// <summary>
    /// 日志追踪值
    /// </summary>
    public interface IlogTracer
    {
        /// <summary>
        /// Gets 追踪Id.
        /// </summary>
        string TraceId { get; }
    }
}