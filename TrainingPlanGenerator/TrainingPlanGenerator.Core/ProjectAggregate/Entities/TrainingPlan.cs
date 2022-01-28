namespace TrainingPlanGenerator.Core.ProjectAggregate.Entities
{
    public class TrainingPlan : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private List<Excersise> _excersises = new List<Excersise>();
        public IEnumerable<Excersise> Excersises => _excersises.AsReadOnly();

        private List<AppUser> _subscribedUsers = new List<AppUser>();
        public IEnumerable<AppUser> SubscribedUsers => _subscribedUsers.AsReadOnly();

        public void AddExcersise(Excersise newExcersise)
        {
            _excersises.Add(newExcersise);
        }
    }
}
