namespace Aspire
{
    /// <summary>
    ///     mapper.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        ///     映射到.
        /// </summary>
        /// <param name="source">源数据.</param>
        /// <typeparam name="TTarget">目标数据类型.</typeparam>
        /// <returns>目标数据.</returns>
        TTarget MapTo<TTarget>(object source);
    }
}