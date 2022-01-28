using AutoMapper;
using TrainingPlanGenerator.Api.ApiModels;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;

namespace TrainingPlanGenerator.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Excersise, ExcersiseApiModel>();

            CreateMap<TrainingPlan, TrainingPlanApiModel>();
        }
    }
}
