using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainingPlanGenerator.Core.ProjectAggregate.Entities;
using TrainingPlanGenerator.Infrastructure.Data;

namespace TrainingPlanGenerator.Web
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            using var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var identityAdmin = await EnsureUser(userManager, "_1Qw23Er45T_", "oleksandra.st3@gmail.com");
            await EnsureRole(roleManager, userManager, identityAdmin, Constants.AdministratorsRole);

            var identityUser = await EnsureUser(userManager, "_1Qw23Er45T_", "example@mail.com");
            await EnsureRole(roleManager, userManager, identityUser, Constants.UsersRole);

            using var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            var trainingPlan = new TrainingPlan()
            {
                Title = "Beginner's Workout",
                Description = "Whether you're just starting out―or starting again―this fast-track workout plan will help you drastically improve your physique and fitness levels."
            };
            trainingPlan.AddExcersise(new Excersise() { Title = "Full-body", Description = String.Empty });
            trainingPlan.AddExcersise(new Excersise() { Title = "Upper body/Lower body", Description = String.Empty });
            trainingPlan.AddExcersise(new Excersise() { Title = "Push/Pull/Legs", Description = String.Empty });

            if (!dbContext.TrainingPlans.Any())
            {
                dbContext.TrainingPlans.Add(trainingPlan);
            }

            var admin = dbContext.AppUsers.FirstOrDefault(x => string.Equals(identityAdmin.Id, x.IdentityUserId));
            if (admin == null)
            {
                admin = new AppUser()
                {
                    FirstName = "Oleksandra",
                    LastName = "Stepankovska",
                    RegistrationDate = DateTime.UtcNow,
                    ActiveTrainingPlan = trainingPlan
                };
                admin.AttachIdentityUser(identityAdmin);
                dbContext.AppUsers.Add(admin);
            }

            var user = dbContext.AppUsers.FirstOrDefault(x => string.Equals(identityUser.Id, x.IdentityUserId));
            if (user == null)
            {
                user = new AppUser()
                {
                    FirstName = "Rick",
                    LastName = "Sanches",
                    RegistrationDate = DateTime.UtcNow
                };
                user.AttachIdentityUser(identityUser);
                dbContext.AppUsers.Add(user);
            }

            dbContext.SaveChanges();
        }

        private static async Task<IdentityUser> EnsureUser(UserManager<IdentityUser> userManager, string password, string username)
        {

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = username,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, password);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user;
        }

        private static async Task<IdentityResult> EnsureRole(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IdentityUser user,
            string role
            )
        {
            IdentityResult IR;

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
