using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.WEB.Models;

namespace TrainingPlanGenerator.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Excersise> _excersiseRepository; 

        public HomeController(ILogger<HomeController> logger, IRepository<Excersise> excersiseRepository)
        {
            _logger = logger;
            _excersiseRepository = excersiseRepository;
        }

        public IActionResult Index()
        {
            var excersise = new Excersise() {
                Title = "Abdominal Crunch"
            };
            _excersiseRepository.Create(excersise);
            _excersiseRepository.SaveChanges();

            var dbExcersises = _excersiseRepository.Get(x => true);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}