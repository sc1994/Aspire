using System.Reflection;
using Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddAspire(Assembly.Load(AppDomain.CurrentDomain.FriendlyName))
    .AddAspireFreeSql<IDatabase>(FreeSql.DataType.Sqlite, "Data Source = App_Data/Business.db", (provider, e, args) =>
    {
        if (args.Sql != null)
        {
            using var scope = provider.CreateScope();
            scope.ServiceProvider.GetRequiredService<Aspire.ILogger>().Info(args.Sql);
        }
    })
    .AddAspireSerilog(configuration => { configuration.WriteTo.Console(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();