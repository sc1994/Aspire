using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAspire(Assembly.Load("AuditAndNoAuth"))
    .AddAspireFreeSql(FreeSql.DataType.Sqlite, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "main.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseAspireSwagger();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
