using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TrainingPlanGenerator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public HomeController(
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
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
