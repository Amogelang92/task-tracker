using LexisApi;
using LexisApi.Models;
using LexisApi.Repository;
using LexisApi.Service;
using LexisApi.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("TaskDb"));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()
        );
});

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigin");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    if (!context.Tasks.Any())
    {
        context.Tasks.AddRange(
            new TaskItem
            {
                Title = "Task 1",
                Description = "Description 1",
                Status = StatusTask.New,
                Priority = TaskPriority.Low,
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Title = "Task 2",
                Description = "Description 2",
                Status = StatusTask.InProgress,
                Priority = TaskPriority.Medium,
                CreatedAt = DateTime.UtcNow
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }