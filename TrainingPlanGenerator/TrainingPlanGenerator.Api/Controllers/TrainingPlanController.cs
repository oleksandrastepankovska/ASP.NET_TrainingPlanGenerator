using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanGenerator.Api.ApiModels;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;

namespace TrainingPlanGenerator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingPlanController : ControllerBase
    {
        private readonly IRepository<TrainingPlan> _trainngPlanRepository;
        private readonly IMapper _mapper;
        public TrainingPlanController(
            IRepository<TrainingPlan> trainngPlanRepository,
            IMapper mapper
            )
        {
            _trainngPlanRepository = trainngPlanRepository;
            _mapper = mapper;
        }

        [Route("count")]
        [HttpGet]
        public async Task<int> Count()
        {
            return _trainngPlanRepository.Get(x => true).Count();
        }

        [Route("list")]
        [HttpGet]
        public async Task<IEnumerable<TrainingPlanApiModel>> List(int skip = 0, int take = 0)
        {
            var trainingPlansList = await _trainngPlanRepository.GetAsync(x => true, x => x.Excersises);
            trainingPlansList = skip > 0 ? trainingPlansList.Skip(skip) : trainingPlansList;
            trainingPlansList = take > 0 ? trainingPlansList.Take(take) : trainingPlansList;

            return _mapper.Map<IEnumerable<TrainingPlanApiModel>>(trainingPlansList);
        }

        [Route("get")]
        [HttpGet]
        public async Task<TrainingPlanApiModel> Get(int id)
        {
            var trainingPlan = await _trainngPlanRepository.GetSingleAsync(x => x.Id == id, x => x.Excersises);

            return  _mapper.Map<TrainingPlanApiModel>(trainingPlan);
        }

        [Route("listbyids")]
        [HttpGet]
        public async Task<IEnumerable<TrainingPlanApiModel>> List(params int[] ids)
        {
            var trainingPlansList = await _trainngPlanRepository.GetAsync(x => ids.Contains(x.Id), x => x.Excersises);

            return _mapper.Map<IEnumerable<TrainingPlanApiModel>>(trainingPlansList);
        }
    }
}
