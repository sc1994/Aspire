using System.Threading.Tasks;

namespace Aspire.Interfaces
{
    /// <summary>
    ///     单一的增查改删.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键.</typeparam>
    /// <typeparam name="TCreateInput">创建输入.</typeparam>
    /// <typeparam name="TGetOutput">输出.</typeparam>
    /// <typeparam name="TUpdateInput">更新输入.</typeparam>
    public interface ICrudSingle<
        TPrimaryKey,
        in TCreateInput,
        TGetOutput,
        in TUpdateInput>
    {
        /// <summary>
        ///     根据创建输入新增数据.
        /// </summary>
        /// <param name="input">创建输入.</param>
        /// <returns>新增的数据主键.</returns>
        Task<TPrimaryKey> CreateAsync(TCreateInput input);

        /// <summary>
        ///     根据主键获取数据.
        /// </summary>
        /// <param name="primaryKey">主键.</param>
        /// <returns>主键对应的数据.</returns>
        Task<TGetOutput> GetAsync(TPrimaryKey primaryKey);

        /// <summary>
        ///     根据更新输入更新数据.
        /// </summary>
        /// <param name="input">更新输入.</param>
        /// <returns>受影响的数据行数.</returns>
        Task<int> UpdateAsync(TUpdateInput input);

        /// <summary>
        ///     根据主键删除数据.
        /// </summary>
        /// <param name="primaryKey">主键.</param>
        /// <returns>受影响的数据行数.</returns>
        Task<int> DeleteAsync(TPrimaryKey primaryKey);
    }
}