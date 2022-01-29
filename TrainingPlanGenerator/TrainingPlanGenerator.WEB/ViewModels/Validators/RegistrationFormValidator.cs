using FluentValidation;

namespace TrainingPlanGenerator.Web.ViewModels.Validators
{
    public class RegistrationFormValidator : AbstractValidator<RegistrationFormViewModel>
    {
        public RegistrationFormValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
