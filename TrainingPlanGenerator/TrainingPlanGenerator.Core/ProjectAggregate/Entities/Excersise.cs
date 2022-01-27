namespace TrainingPlanGenerator.Core.ProjectAggregate.Entities
{
    public class Excersise : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        private List<TrainingPlan> _trainingPlans = new List<TrainingPlan>();
        public IEnumerable<TrainingPlan> TrainingPlans => _trainingPlans.AsReadOnly();
    }
}
