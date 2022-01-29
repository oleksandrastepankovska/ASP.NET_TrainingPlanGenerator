using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;

namespace TrainingPlanGenerator.Infrastructure.Data.Config
{
    internal class ExcersiseConfiguration : IEntityTypeConfiguration<Excersise>
    {
        public void Configure(EntityTypeBuilder<Excersise> builder)
        {
        }
    }
}
