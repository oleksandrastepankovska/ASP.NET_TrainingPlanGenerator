using AutoMapper;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Web.ViewModels;

namespace TrainingPlanGenerator.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Excersise, ExcersiseViewModel>();
            CreateMap<TrainingPlan, TrainingPlanViewModel>();
        }
    }
}
