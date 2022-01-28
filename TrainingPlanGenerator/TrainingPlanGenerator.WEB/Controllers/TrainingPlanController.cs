﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Web.ViewModels;

namespace TrainingPlanGenerator.Web.Controllers
{
    public class TrainingPlanController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TrainingPlan> _trainingPlanRepository;

        public TrainingPlanController(
            IMapper mapper,
            IRepository<TrainingPlan> trainingPlanRepository
            )
        {
            _mapper = mapper;
            _trainingPlanRepository = trainingPlanRepository;
        }

        [HttpGet("trainingplan")]
        public async Task<IActionResult> Index(int id)
        {
            var trainingPlan = await _trainingPlanRepository.GetSingleAsync(x => x.Id == id, x => x.Excersises);
            var trainingPlanViewModel = _mapper.Map<TrainingPlanViewModel>(trainingPlan);
            return View(trainingPlanViewModel);
        }
    }
}
