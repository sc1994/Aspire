using Microsoft.AspNetCore.Mvc;

namespace Aspire.Application.AppServices
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public abstract class AppService : ControllerBase
    {

    }
}
