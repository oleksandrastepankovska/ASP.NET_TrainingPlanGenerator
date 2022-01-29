namespace TrainingPlanGenerator.Web.ViewModels
{
    public class TrainingPlanViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ExcersiseViewModel> Excersises { get; set; } = new List<ExcersiseViewModel>();
    }
}
