using Hangfire.MemoryStorage;
using Hangfire;
using JobSchedulerService.Managers;
using JobSchedulerService.Repository;
using JobSchedulerService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Hangfire
GlobalConfiguration.Configuration.UseMemoryStorage();
builder.Services.AddHangfire(c => c.UseMemoryStorage());
JobStorage.Current = new MemoryStorage();
builder.Services.AddHangfireServer();

//Services
builder.Services.AddSingleton<IJobManager, JobManager>();
builder.Services.AddSingleton<IJobRepository, JobRepository>();
builder.Services.AddSingleton<ISortingService, SortingService>();

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

app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

app.UseAuthorization();

app.MapControllers();

app.Run();
