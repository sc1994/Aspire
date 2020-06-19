using Aspire.Application.AppServices;
using Aspire.Domain.Repositories;
using Aspire.Map;

using Demo.Core.Blogs;

namespace Demo.Application.Blogs
{
    public class BolgAppService : CrudAppService<BlogEntity, BlogOutputDto, long, BlogCreateDto, BlogUpdateDto>
    {
        public BolgAppService(IRepositoryEfCore<BlogEntity, long> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }
}
