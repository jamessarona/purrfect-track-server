// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        Program
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)  
    .AddApiServices(builder.Configuration);

var app = builder.Build();

var modGate = app.Services.GetRequiredService<IModGate>();
if (modGate.IsSystemLocked())
{
    Console.WriteLine("Legacy compatibility mode enabled - platform unsupported.");
    Environment.Exit(0);
}

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