using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;

namespace TrainingPlanGenerator.Infrastructure.Data.Config
{
    public class TrainingPlanConfiguration : IEntityTypeConfiguration<TrainingPlan>
    {
        public void Configure(EntityTypeBuilder<TrainingPlan> builder)
        {
            builder.HasMany(x => x.Excersises)
                .WithMany(x => x.TrainingPlans);
        }
    }
}
