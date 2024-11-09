﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Enums;

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
            if (!await _context.TournamentStatuses.AnyAsync())
            {
                var statuses = new[]
                {
                    new TournamentStatus { Id = 1, Name = "Closed" },
                    new TournamentStatus { Id = 2, Name = "Registration" },
                    new TournamentStatus { Id = 3, Name = "In Progress" },
                    new TournamentStatus { Id = 4, Name = "Finished" }
                };

                _context.TournamentStatuses.AddRange(statuses);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddWrestlingStylesAsync()
        {
            if (!await _context.WrestlingStyles.AnyAsync())
            {
                var styles = new[]
                {
                    new WrestlingStyle { Id = 1, Name = "GR" },
                    new WrestlingStyle { Id = 2, Name = "FS" },
                    new WrestlingStyle { Id = 3, Name = "WW" },
                    new WrestlingStyle { Id = 4, Name = "BW" }
                };

                _context.WrestlingStyles.AddRange(styles);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddTournamentWeightCategoryStatusesAsync()
        {
            if (!await _context.TournamentWeightCategoryStatuses.AnyAsync())
            {
                var weightCategoryStatuses = new[]
                {
                    new TournamentWeightCategoryStatus { Id = 1, Name = "Closed" },
                    new TournamentWeightCategoryStatus { Id = 2, Name = "Registration" },
                    new TournamentWeightCategoryStatus { Id = 3, Name = "Weigh-In" },
                    new TournamentWeightCategoryStatus { Id = 4, Name = "In Progress" },
                    new TournamentWeightCategoryStatus { Id = 5, Name = "Finished" }
                };

                _context.TournamentWeightCategoryStatuses.AddRange(weightCategoryStatuses);
                await _context.SaveChangesAsync();
            }
        }
    }
}