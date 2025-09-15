using cleanArchitecture.Application;
using cleanArchitecture.Infrastructure;
using cleanArchitecture.Preentation2.Extensions;
using cleanArchitecture.presentation2;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var configuration = builder.Configuration;

builder
    .Services
    .AddApplication()
    .AddInfrastructure(configuration)
    .AddPresentation();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
app.ApplyMigrations();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

