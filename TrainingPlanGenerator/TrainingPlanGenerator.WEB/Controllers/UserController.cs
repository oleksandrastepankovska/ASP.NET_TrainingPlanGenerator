using System.Transactions;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainingPlanGenerator.Core.Interfaces;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Web.ViewModels;
using TrainingPlanGenerator.Web.ViewModels.Validators;

namespace TrainingPlanGenerator.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IValidator<RegistrationFormViewModel> _registrationFormValidator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepository<AppUser> _userRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserController(
            RegistrationFormValidator registrationFormValidator,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRepository<AppUser> userRepository,
            SignInManager<IdentityUser> signInManager
            )
        {
            _registrationFormValidator = registrationFormValidator;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        [HttpGet("signin")]
        public async Task<IActionResult> SignIn()
        {
            var signIn = await _signInManager.PasswordSignInAsync("example@mail.com", "_1Qw23Er45T_", false, false);

            var action = nameof(UserController.Profile);
            var controller = nameof(UserController).Replace(nameof(Controller), "");
            return RedirectToAction(action, controller);
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            var registrationPageViewModel = new RegisterPageViewModel();
            return View(registrationPageViewModel);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationFormViewModel model)
        {
            var validationResult = await _registrationFormValidator.ValidateAsync(model);
            validationResult.AddToModelState(ModelState, null);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var identityUser = await _userManager.FindByNameAsync(model.Email);
                if (identityUser == null)
                {
                    identityUser = new IdentityUser
                    {
                        UserName = model.Email,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(identityUser, model.Password);
                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(identityUser, Constants.UsersRole);
                        if (roleResult.Succeeded)
                        {
                            var user = _mapper.Map<AppUser>(model);
                            user.AttachIdentityUser(identityUser);

                            _userRepository.Create(user);
                            _userRepository.SaveChanges();

                            scope.Complete();
                            var action = nameof(HomeController.Index);
                            var controller = nameof(HomeController).Replace(nameof(Controller), "");
                            return RedirectToAction(action, controller);
                        }
                        scope.Dispose();
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError(String.Empty, $"{error.Code} - {error.Description}");
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, $"{error.Code} - {error.Description}");
                    }
                }
                ModelState.AddModelError(nameof(model.Email), $"User {model.Email} already existis");
            }
            catch (Exception ex)
            {
                scope.Dispose();
                throw ex;
            }

            var registrationPageViewModel = new RegisterPageViewModel();
            registrationPageViewModel.RegistrationForm = model;
            return View(registrationPageViewModel);
        }

        [HttpGet("profile")]
        [Authorize(Roles = $"{Constants.UsersRole},{Constants.AdministratorsRole}")]
        public async Task<IActionResult> Profile()
        {
            var idenityUser = await _userManager.GetUserAsync(User);
            var user = await _userRepository.GetSingleAsync(x => string.Equals(idenityUser.Id, x.IdentityUserId));

            var profilePageViewModel = new ProfilePageViewModel();
            profilePageViewModel.User = _mapper.Map<UserViewModel>(user);

            return View(profilePageViewModel);
        }
    }
}
