using System.Collections.Generic;

namespace Aspire.Map
{
    public interface IAspireMapper
    {
        TTarget To<TTarget>(object source);
        TTarget To<TSource, TTarget>(TSource source, TTarget target);
        IEnumerable<TTarget> To<TTarget>(IList<object> source);
    }
}
