using PurrfectTrack.Api;
using PurrfectTrack.Application;
using PurrfectTrack.Infrastructure;
using PurrfectTrack.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)  
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();


// Run in Package Manager Console for Manual Migration Command
// Add-Migration InitialCreate -OutputDir Data/Migrations -Project PurrfectTrack.Infrastructure -StartupProject PurrfectTrack.Api
// Update-Database