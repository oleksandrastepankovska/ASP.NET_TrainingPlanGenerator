using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Infrastructure;
using TrainingPlanGenerator.Infrastructure.Data;
using TrainingPlanGenerator.Web;
using Microsoft.AspNetCore.Identity;
using TrainingPlanGenerator.Web.ViewModels.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddFluentValidation(fvconfig => {
        fvconfig.RegisterValidatorsFromAssemblyContaining<RegistrationFormValidator>(lifetime: ServiceLifetime.Scoped);
        fvconfig.AutomaticValidationEnabled = false;
    });

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

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
        throw ex;
    }
}

app.Run();