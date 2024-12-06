using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using TaskManagement.Application.Handlers;
using TaskManagement.Application.Integrations;
using TaskManagement.Application.Repositories;
using TaskManagement.Infrastructure;
using TaskManagement.Infrastructure.Configuration;
using TaskManagement.Infrastructure.Integrations;
using TaskManagement.Infrastructure.Mapping;
using TaskManagement.Infrastructure.Repositories;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.Configure<ServiceBusSettings>(builder.Configuration.GetSection("ServiceBusSettings"));

// Add services to the container.
builder.Services.AddControllers();

AddSwagger(builder);
AddAutoMapper(builder);
AddServices(builder);
AddServiceBus(builder);
AddCors(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();

static void AddServiceBus(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton(provider =>
    {
        var settings = provider.GetRequiredService<IOptions<ServiceBusSettings>>().Value;
        return new ServiceBusClient(settings.ConnectionString);
    });

    builder.Services.AddSingleton<IServiceBusHandler, AzureServiceBusHandler>();
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ITaskReadonlyRepository, TaskReadonlyRepository>();
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
    builder.Services.AddDbContext<TaskManagementContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagementDb")));

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommandHandler).Assembly));
}

static void AddSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Task Management API",
            Version = "v1",
            Description = "An API to manage tasks with status transitions"
        });
    });
}

static void AddAutoMapper(WebApplicationBuilder builder)
{
    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile<TasksProfile>();
    });
}

static void AddCors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}