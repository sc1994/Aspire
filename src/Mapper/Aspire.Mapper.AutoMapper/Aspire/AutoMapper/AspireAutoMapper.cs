using AutoMapper;

namespace Aspire.AutoMapper
{
    /// <summary>
    ///     使用 auto mapper 实现 aspire mapper.
    /// </summary>
    public class AspireAutoMapper : IAspireMapper
    {
        private readonly IMapper mapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AspireAutoMapper" /> class.
        /// </summary>
        /// <param name="mapper">auto mapper.</param>
        public AspireAutoMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public TTarget MapTo<TTarget>(object source)
        {
            return mapper.Map<TTarget>(source);
        }

        /// <inheritdoc />
        public void MapTo<TSource, TTarget>(TSource source, ref TTarget target)
        {
            target = mapper.Map<TSource, TTarget>(source, target);
        }
    }
}