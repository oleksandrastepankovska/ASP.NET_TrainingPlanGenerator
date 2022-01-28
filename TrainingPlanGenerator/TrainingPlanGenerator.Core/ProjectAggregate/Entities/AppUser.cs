using Microsoft.AspNetCore.Identity;

namespace TrainingPlanGenerator.Core.ProjectAggregate.Entities
{
    public class AppUser : BaseEntity
    {
        public string IdentityUserId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public TrainingPlan ActiveTrainingPlan { get; set; }

        public void AttachIdentityUser(IdentityUser identityUser)
        {
            IdentityUserId = identityUser.Id;
        }
    }
}
