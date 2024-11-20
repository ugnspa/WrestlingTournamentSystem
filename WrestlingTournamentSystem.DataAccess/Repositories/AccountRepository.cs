﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly WrestlingTournamentSystemDbContext _context;

        public AccountRepository(WrestlingTournamentSystemDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task CreateUserAsync(User user, string password)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var createUserResult = await _userManager.CreateAsync(user, password);
                if (!createUserResult.Succeeded)
                    throw new BusinessRuleValidationException(createUserResult.Errors.First().Description);

                var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.TournamentOrganiser); //Pass role as parameter
                if (!addRoleResult.Succeeded)
                    throw new BusinessRuleValidationException(addRoleResult.Errors.First().Description);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                throw new Exception("An error occurred creating User and adding Roles.");
            }
        }

        public async Task<User?> FindByUsernameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> IsPasswordValidAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public Task<User?> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetCoaches()
        {
            var coaches = await _userManager.GetUsersInRoleAsync(UserRoles.Coach);

            return coaches;
        }

        public async Task<User?> GetCoachWithWrestlersAsync(string userId)
        {
            var coach = await _context.Users
                .Where(user =>  user.Id == userId)
                .Include(coach => coach.Wrestlers)
                .ThenInclude(wrestler => wrestler.WrestlingStyle)
                .FirstOrDefaultAsync();           

            if (coach == null)
                return null;

            var isCoach = await _userManager.IsInRoleAsync(coach, UserRoles.Coach);

            if (!isCoach)
                return null;

            return coach;
        }
    }
}
