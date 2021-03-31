using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aspire.Cli.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public List<Provider> GetProviders()
        {

        }
    }

    public class Provider
    {
        public bool Required { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
