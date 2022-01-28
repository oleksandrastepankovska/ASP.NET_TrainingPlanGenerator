using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Infrastructure.Data.Config;

namespace TrainingPlanGenerator.Infrastructure.Data
{
    //dotnet ef migrations add InitialCreate --project TrainingPlanGenerator.Infrastructure --startup-project TrainingPlanGenerator.Web --output-dir Data\Migrations
    //dotnet ef database update --project TrainingPlanGenerator.Infrastructure --startup-project TrainingPlanGenerator.Web
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Excersise> Excersises => Set<Excersise>();
        public DbSet<TrainingPlan> TrainingPlans => Set<TrainingPlan>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ExcersiseConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingPlanConfiguration());
        }
    }
}
