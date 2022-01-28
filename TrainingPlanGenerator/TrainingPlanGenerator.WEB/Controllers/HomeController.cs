using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Web.ViewModels;

namespace TrainingPlanGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TrainingPlan> _trainingPlanRepository;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(
            IMapper mapper,
            IRepository<TrainingPlan> trainingPlanRepository,
            SignInManager<IdentityUser> signInManager)
        {

            _mapper = mapper;
            _trainingPlanRepository = trainingPlanRepository;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Index()
        {
            var trainingPlansList = await _trainingPlanRepository.GetAsync(x => true);

            var pageViewModel = new HomePageViewModel();
            pageViewModel.TrainingPlans = _mapper.Map<IEnumerable<TrainingPlanViewModel>>(trainingPlansList).ToList();

            return View(pageViewModel);
        }

        [HttpGet("SignIn")]
        public async Task<string> SignIn()
        {
            var signIn = await _signInManager.PasswordSignInAsync("example@mail.com", "_1Qw23Er45T_", false, false);

            return $"{signIn.Succeeded}";
        }

        [Authorize(Roles = Constants.UsersRole)]
        public async Task<string> AuthAcces()
        {
            return "Success";
        }
    }
}
