using FluentValidation;

namespace TrainingPlanGenerator.Web.ViewModels.Validators
{
    public class SignInFormValidator : AbstractValidator<SignInFormViewModel>
    {
        public SignInFormValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
