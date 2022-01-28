using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;

namespace TrainingPlanGenerator.Infrastructure.Data.Config
{
    internal class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IdentityUserId)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();

            builder.Property(x => x.RegistrationDate)
                .IsRequired();

            builder.HasOne(x => x.ActiveTrainingPlan)
                .WithMany(x => x.SubscribedUsers);
        }
    }
}
