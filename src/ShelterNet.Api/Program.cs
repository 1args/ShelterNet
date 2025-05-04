using Scalar.AspNetCore;
using ShelterNet.Api.Extensions;
using ShelterNet.Application.Extensions;
using ShelterNet.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

services
    .AddAuthenticationRules(configuration)
    .AddInfrastructure(configuration)
    .AddApplication()
    .AddApi(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
