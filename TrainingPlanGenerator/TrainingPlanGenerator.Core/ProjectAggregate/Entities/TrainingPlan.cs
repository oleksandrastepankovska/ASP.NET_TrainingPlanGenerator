namespace TrainingPlanGenerator.Core.ProjectAggregate.Entities
{
    public class TrainingPlan : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        private List<Excersise> _excersises = new List<Excersise>();
        public IEnumerable<Excersise> Excersises => _excersises.AsReadOnly();

        public void AddExcersise(Excersise newExcersise)
        {
            _excersises.Add(newExcersise);
        }
    }
}
