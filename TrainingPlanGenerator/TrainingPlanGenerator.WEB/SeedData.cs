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
            using (var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (!dbContext.TrainingPlans.Any())
                {
                    var trainingPlan = new TrainingPlan()
                    {
                        Title = "Beginner's Workout",
                        Description = "Whether you're just starting out―or starting again―this fast-track workout plan will help you drastically improve your physique and fitness levels."
                    };
                    trainingPlan.AddExcersise(new Excersise() { Title = "Full-body" });
                    trainingPlan.AddExcersise(new Excersise() { Title = "Upper body/Lower body" });
                    trainingPlan.AddExcersise(new Excersise() { Title = "Push/Pull/Legs" });

                    dbContext.TrainingPlans.Add(trainingPlan);
                }

                if (!dbContext.Users.Any() && !dbContext.UserRoles.Any())
                {
                    var adminId = await EnsureUser(serviceProvider, "_1Qw23Er45T_", "oleksandra.st3@gmail.com");
                    await EnsureRole(serviceProvider, adminId, Constants.AdministratorsRole);

                    var userId = await EnsureUser(serviceProvider, "_1Qw23Er45T_", "example@mail.com");
                    await EnsureRole(serviceProvider, userId, Constants.UsersRole);
                }                

                dbContext.SaveChanges();
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
