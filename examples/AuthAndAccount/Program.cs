using System.Reflection;
using AuthAndAccount.Core.Accounts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddAspire(Assembly.Load(AppDomain.CurrentDomain.FriendlyName))
    .AddAspireSerilog(configuration =>
    {
        
    })
    .AddAspireAuth<Account, AccountManage>(provider =>
    {
        return provider.GetRequiredService<AccountManage>();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseAspireSwagger();
}

app.UseHttpsRedirection();
app.UseAspireFriendlyException();

app.UseAuthorization();

app.MapControllers();

app.Run();
