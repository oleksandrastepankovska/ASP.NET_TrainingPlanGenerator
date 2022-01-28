namespace TrainingPlanGenerator.Api.ApiModels
{
    public class TrainingPlanApiModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ExcersiseApiModel> Excersises { get; set; } = new List<ExcersiseApiModel>();
    }
}
