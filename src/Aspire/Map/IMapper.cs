using System.Collections.Generic;

namespace Aspire.Map
{
    public interface IMapper
    {
        TTarget To<TTarget>(object source);
        TTarget To<TSource, TTarget>(TSource source, TTarget target);
        IEnumerable<TTarget> To<TTarget>(IList<object> source);
        IEnumerable<TTarget> To<TSource, TTarget>(IList<TSource> source, IList<TTarget> target);
    }
}
