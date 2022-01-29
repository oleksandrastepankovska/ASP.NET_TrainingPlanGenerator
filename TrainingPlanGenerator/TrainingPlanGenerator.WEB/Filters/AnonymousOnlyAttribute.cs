using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrainingPlanGenerator.Web.Controllers;

namespace TrainingPlanGenerator.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult(
                    nameof(UserController.Profile),
                    nameof(UserController).Replace(nameof(Controller), string.Empty),
                    null);
            }
        }
    }
}
