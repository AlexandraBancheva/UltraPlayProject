using Microsoft.AspNetCore.Builder;
using UltraPlayProject.Application.Interfaces;
using UltraPlayProject.Domain.Interfaces;
using UltraPlayProject.Domain.Services;
using UltraPlayProject.Persistence;
using UltraPlayProject.Persistence.PeriodicBackgroundTask;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUltraPlayRepository, UltraPlayRepository>();
builder.Services.AddTransient<IUltraPlayDatabaseRepository, UltraPlayDatabaseRepository>();
builder.Services.AddTransient<IUltraPlayProjectService, UltraPlayProjectService>();

builder.Services.AddScoped<UpdatingDatabase>();
builder.Services.AddSingleton<PeriodicHostedService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<PeriodicHostedService>());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapMethods("/background", new[] { "PATCH" }, 
(
    PeriodicHostedServiceState state,
    PeriodicHostedService service) =>
{
    service.IsEnabled = state.IsEnabled;
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
