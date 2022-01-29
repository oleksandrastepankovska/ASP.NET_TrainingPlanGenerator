using AutoMapper;
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
        private readonly IRepository<AppUser> _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            IMapper mapper,
            IRepository<TrainingPlan> trainingPlanRepository,
            IRepository<AppUser> userRepository,
            UserManager<IdentityUser> userManager)
        {

            _mapper = mapper;
            _trainingPlanRepository = trainingPlanRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var pageViewModel = new HomePageViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var idenityUser = await _userManager.GetUserAsync(User);
                var user = await _userRepository.GetSingleAsync(x => string.Equals(idenityUser.Id, x.IdentityUserId));

                pageViewModel.User = _mapper.Map<UserViewModel>(user);
            }

            var trainingPlansList = await _trainingPlanRepository.GetAsync(x => true);

            pageViewModel.TrainingPlans = _mapper.Map<IEnumerable<TrainingPlanViewModel>>(trainingPlansList).ToList();

            return View(pageViewModel);
        }
    }
}
