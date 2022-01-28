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

            CreateMap<RegistrationFormViewModel, AppUser>()
                .ForMember(u => u.FirstName, opts => opts.MapFrom(rf => rf.FirstName))
                .ForMember(u => u.LastName, opts => opts.MapFrom(rf => rf.LastName))
                .ForMember(u => u.RegistrationDate, opts => opts.MapFrom(rf => DateTime.UtcNow));

            CreateMap<AppUser, UserViewModel>();
        }
    }
}
