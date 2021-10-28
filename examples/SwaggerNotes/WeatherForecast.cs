namespace SwaggerNotes;

public class WeatherForecast : IWeatherForecast
{
    /// <summary>
    /// 注释 D.
    /// </summary>
    public DateTime Date { get; set; }

    /// <inheritdoc/>
    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}


public interface IWeatherForecast
{
    /// <summary>
    /// 注释 X.
    /// </summary>
    int TemperatureC { get; set; }
}
