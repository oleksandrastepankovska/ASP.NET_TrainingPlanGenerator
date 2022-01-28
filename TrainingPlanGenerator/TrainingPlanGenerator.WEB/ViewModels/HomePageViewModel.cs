namespace TrainingPlanGenerator.Web.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        public List<TrainingPlanViewModel> TrainingPlans { get; set; } = new List<TrainingPlanViewModel>();
    }
}
