namespace TrainingPlanGenerator.Web.ViewModels
{
    public class TrainingPlanViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ExcersiseViewModel> Excersises { get; set; } = new List<ExcersiseViewModel>();
    }
}
