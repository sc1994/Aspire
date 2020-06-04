using System;

namespace Aspire.Application.AppServices.Dtos
{
    public class CommonDto : CommonDto<long>
    {
    }

    public class CommonDto<TId> : OutputDto<TId>
    {

    }
}
