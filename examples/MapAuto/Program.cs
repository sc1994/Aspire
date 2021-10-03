using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAspire(Assembly.Load("MapAuto"))
    .AddAspireAutoMapper(Assembly.Load("MapAuto"));

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
