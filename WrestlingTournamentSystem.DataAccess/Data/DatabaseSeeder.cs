using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;

namespace WrestlingTournamentSystem.DataAccess.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WrestlingTournamentSystemDbContext _context;
        public DatabaseSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, WrestlingTournamentSystemDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRolesAsync();
            await AddAdminUserAsync();
            await AddCoachUserAsync();
            await AddOrganiserUserAsync();
            await AddTournamentStatusesAsync();
            await AddWrestlingStylesAsync();
            await AddTournamentWeightCategoryStatusesAsync();
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

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null) 
            {
                var createAdminResult = await _userManager.CreateAsync(newAdminUser, "Password1!"); //Change to env
                if (createAdminResult.Succeeded) 
                {
                    await _userManager.AddToRolesAsync(newAdminUser, UserRoles.All);
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

            var existingCoachUser = await _userManager.FindByNameAsync(newCoachUser.UserName);
            if (existingCoachUser == null)
            {
                var createCoachResult = await _userManager.CreateAsync(newCoachUser, "Password1!"); //Change to env
                if (createCoachResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newCoachUser, UserRoles.Coach);
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

            var existingOrganiserUser = await _userManager.FindByNameAsync(newOrganiserUser.UserName);
            if (existingOrganiserUser == null)
            {
                var createOrganiserResult = await _userManager.CreateAsync(newOrganiserUser, "Password1!"); //Change to env
                if (createOrganiserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newOrganiserUser, UserRoles.TournamentOrganiser);
                }
            }
        }

        private async Task AddDefaultRolesAsync()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task AddTournamentStatusesAsync()
        {
            var statuses = new[]
                {
                    new TournamentStatus { Id = 1, Name = "Closed" },
                    new TournamentStatus { Id = 2, Name = "Registration" },
                    new TournamentStatus { Id = 3, Name = "In Progress" },
                    new TournamentStatus { Id = 4, Name = "Finished" }
                };

            foreach (var status in statuses) 
            {
                var existingStatus = await _context.TournamentStatuses.FindAsync(status.Id);
                if (existingStatus == null)
                {
                    _context.TournamentStatuses.Add(status);
                }
            }

            await _context.SaveChangesAsync();           
        }

        private async Task AddWrestlingStylesAsync()
        {
            var styles = new[]
{
                    new WrestlingStyle { Id = 1, Name = "GR" },
                    new WrestlingStyle { Id = 2, Name = "FS" },
                    new WrestlingStyle { Id = 3, Name = "WW" },
                    new WrestlingStyle { Id = 4, Name = "BW" }
                };

            foreach(var style in styles)
            {
                var existingStyle = await _context.WrestlingStyles.FindAsync(style.Id);
                if (existingStyle == null)
                {
                    _context.WrestlingStyles.Add(style);
                }
            }

            await _context.SaveChangesAsync();       
        }

        private async Task AddTournamentWeightCategoryStatusesAsync()
        {
            var weightCategoryStatuses = new[]
               {
                    new TournamentWeightCategoryStatus { Id = 1, Name = "Closed" },
                    new TournamentWeightCategoryStatus { Id = 2, Name = "Registration" },
                    new TournamentWeightCategoryStatus { Id = 3, Name = "Weigh-In" },
                    new TournamentWeightCategoryStatus { Id = 4, Name = "In Progress" },
                    new TournamentWeightCategoryStatus { Id = 5, Name = "Finished" }
                };

            foreach (var status in weightCategoryStatuses)
            {
                var existingStatus = await _context.TournamentWeightCategoryStatuses.FindAsync(status.Id);
                if (existingStatus == null)
                {
                    _context.TournamentWeightCategoryStatuses.Add(status);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
