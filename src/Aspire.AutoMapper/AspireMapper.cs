using System.Collections.Generic;

using Aspire.Map;

using AutoMapper;

namespace Aspire.AutoMapper
{
    public class AspireMapper : IAspireMapper
    {
        private readonly IMapper _mapper;

        public AspireMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget To<TTarget>(object source)
        {
            return _mapper.Map<TTarget>(source);
        }

        public TTarget To<TSource, TTarget>(TSource source, TTarget target)
        {
            return _mapper.Map(source, target);
        }

        public IEnumerable<TTarget> To<TTarget>(IList<object> source)
        {
            foreach (var item in source)
            {
                yield return To<TTarget>(item);
            }
        }
    }
}
