using Aspire;
using Aspire.Entity;
using Microsoft.AspNetCore.Mvc;
using ILogger = Aspire.ILogger;

namespace Repository.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepository<TableA, Guid, IDatabase> repoistory;
    private readonly ILogger logger;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastController(IRepository<TableA, Guid, IDatabase> repoistory, ILogger logger)
    {
        this.repoistory = repoistory;
        this.logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        logger.Info("111");
        repoistory.Select().Where(x => x.CreatedAt > DateTime.Now).ToList();
        logger.Info("222");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}


public class TableA : EntityFullAudit, IEntityBase<Guid, IDatabase>
{

}
