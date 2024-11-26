using Microsoft.AspNetCore.Identity;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;

namespace WrestlingTournamentSystem.DataAccess.Data
{
    public class DatabaseSeeder(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        WrestlingTournamentSystemDbContext context)
    {
        public async Task SeedAsync()
        {
            await AddDefaultRolesAsync();
            await AddAdminUserAsync();
            await AddCoachUserAsync();
            await AddOrganiserUserAsync();
            await AddTournamentStatusesAsync();
            await AddWrestlingStylesAsync();
            await AddTournamentWeightCategoryStatusesAsync();
            await AddPrimaryWeightCategoriesAsync();
        }

        private async Task AddAdminUserAsync()
        {
            var newAdminUser = new User
            {
                UserName = "admin",
                Email = "admin@admin.com",
                Name = "Admin",
                Surname = "Admin",
                City = "AdminCity"
            };

            var existingAdminUser = await userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null) 
            {
                var createAdminResult = await userManager.CreateAsync(newAdminUser, "Password1!"); //Change to env
                if (createAdminResult.Succeeded) 
                {
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }

        private async Task AddCoachUserAsync() 
        {
            var newCoachUser = new User
            {
                UserName = "coach",
                Email = "coach@coach.com",
                Name = "Coach",
                Surname = "Coach",
                City = "CoachCity"
            };

            var existingCoachUser = await userManager.FindByNameAsync(newCoachUser.UserName);
            if (existingCoachUser == null)
            {
                var createCoachResult = await userManager.CreateAsync(newCoachUser, "Password1!"); //Change to env
                if (createCoachResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newCoachUser, UserRoles.Coach);
                }
            }
        }

        private async Task AddOrganiserUserAsync()
        {
            var newOrganiserUser = new User
            {
                UserName = "organiser",
                Email = "organiser@organiser.com",
                Name = "Organiser",
                Surname = "Organiser",
                City = "OrganiserCity"
            };

            var existingOrganiserUser = await userManager.FindByNameAsync(newOrganiserUser.UserName);
            if (existingOrganiserUser == null)
            {
                var createOrganiserResult = await userManager.CreateAsync(newOrganiserUser, "Password1!"); //Change to env
                if (createOrganiserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(newOrganiserUser, UserRoles.TournamentOrganiser);
                }
            }
        }

        private async Task AddDefaultRolesAsync()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task AddTournamentStatusesAsync()
        {
            var statuses = new[]
                {
                    new TournamentStatus { Name = "Closed" },
                    new TournamentStatus { Name = "Registration" },
                    new TournamentStatus { Name = "In Progress" },
                    new TournamentStatus { Name = "Finished" }
                };

            foreach (var status in statuses) 
            {
                var existingStatus = await context.TournamentStatuses.FindAsync(status.Id);
                if (existingStatus == null)
                {
                    context.TournamentStatuses.Add(status);
                }
            }

            await context.SaveChangesAsync();           
        }

        private async Task AddWrestlingStylesAsync()
        {
            var styles = new[]
{
                    new WrestlingStyle { Name = "GR" },
                    new WrestlingStyle { Name = "FS" },
                    new WrestlingStyle { Name = "WW" },
                    new WrestlingStyle { Name = "BW" }
                };

            foreach(var style in styles)
            {
                var existingStyle = await context.WrestlingStyles.FindAsync(style.Id);
                if (existingStyle == null)
                {
                    context.WrestlingStyles.Add(style);
                }
            }

            await context.SaveChangesAsync();       
        }

        private async Task AddTournamentWeightCategoryStatusesAsync()
        {
            var weightCategoryStatuses = new[]
               {
                    new TournamentWeightCategoryStatus { Name = "Closed" },
                    new TournamentWeightCategoryStatus { Name = "Registration" },
                    new TournamentWeightCategoryStatus { Name = "Weigh-In" },
                    new TournamentWeightCategoryStatus { Name = "In Progress" },
                    new TournamentWeightCategoryStatus { Name = "Finished" }
                };

            foreach (var status in weightCategoryStatuses)
            {
                var existingStatus = await context.TournamentWeightCategoryStatuses.FindAsync(status.Id);
                if (existingStatus == null)
                {
                    context.TournamentWeightCategoryStatuses.Add(status);
                }
            }

            await context.SaveChangesAsync();
        }

        private async Task AddPrimaryWeightCategoriesAsync()
        {
            var grStyle = context.WrestlingStyles.FirstOrDefault(s => s.Name == "GR");

            if (grStyle == null) return;

            var primaryWeightCategories = new[]
            {
                    new WeightCategory { Weight = 60, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 63, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 67, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 72, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 77, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 82, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 87, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 92, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 97, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle },
                    new WeightCategory { Weight = 130, Age = "Seniors", PrimaryCategory = true, WrestlingStyle = grStyle }
            };

            foreach (var wc in primaryWeightCategories)
            {
                var existingWeightCategory = await context.WeightCategories.FindAsync(wc.Id);
                if (existingWeightCategory == null)
                {
                    context.WeightCategories.Add(wc);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
