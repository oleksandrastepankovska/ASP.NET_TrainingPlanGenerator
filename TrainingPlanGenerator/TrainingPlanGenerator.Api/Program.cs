using AutoMapper;
using TrainingPlanGenerator.Api;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Infrastructure;
using TrainingPlanGenerator.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext(connectionString);

builder.Services.AddScoped<IRepository<Excersise>, Repository<Excersise>>();
builder.Services.AddScoped<IRepository<TrainingPlan>, Repository<TrainingPlan>>();
builder.Services.AddScoped<IRepository<AppUser>, Repository<AppUser>>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
