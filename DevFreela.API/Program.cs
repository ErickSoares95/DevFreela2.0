using DevFreela.API.ExceptionHandler;
using DevFreela.Application;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<FreelanceTotalCostConfig>(builder.Configuration.GetSection("FreelanceTotalCostConfig"));

//builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreelaDb"));

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

//Configura��es execeptionhandler
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
