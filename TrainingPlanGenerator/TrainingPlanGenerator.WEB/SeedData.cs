using Microsoft.EntityFrameworkCore;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Infrastructure.Data;

namespace TrainingPlanGenerator.Web
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (dbContext.TrainingPlans.Any())
                {
                    return;
                }

                var trainingPlan = new TrainingPlan() {
                    Title = "Beginner's Workout",
                    Description = "Whether you're just starting out―or starting again―this fast-track workout plan will help you drastically improve your physique and fitness levels."
                };
                trainingPlan.AddExcersise(new Excersise() { Title = "Full-body" });
                trainingPlan.AddExcersise(new Excersise() { Title = "Upper body/Lower body" });
                trainingPlan.AddExcersise(new Excersise() { Title = "Push/Pull/Legs" });

                dbContext.TrainingPlans.Add(trainingPlan);
            }
        }
    }
}
