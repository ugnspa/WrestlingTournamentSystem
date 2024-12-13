using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class AccountRepository(WrestlingTournamentSystemDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        : IAccountRepository
    {
        public async Task CreateUserAsync(User user, string password, string roleId)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var createUserResult = await userManager.CreateAsync(user, password);
                if (!createUserResult.Succeeded)
                    throw new BusinessRuleValidationException(createUserResult.Errors.First().Description);

                var role = await roleManager.FindByIdAsync(roleId);
                if (role == null)
                    throw new BusinessRuleValidationException("Role not found");

                var addRoleResult = await userManager.AddToRoleAsync(user, role.Name!);
                if (!addRoleResult.Succeeded)
                    throw new BusinessRuleValidationException(addRoleResult.Errors.First().Description);

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                throw new BusinessRuleValidationException($"An error occurred creating User and adding Roles. {e.Message}");
            }
        }

        public async Task<User?> FindByUsernameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<bool> IsPasswordValidAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public Task<User?> FindByIdAsync(string userId)
        {
            return userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetCoaches()
        {
            var coaches = await userManager.GetUsersInRoleAsync(UserRoles.Coach);

            return coaches;
        }

        public async Task<User?> GetCoachWithWrestlersAsync(string userId)
        {
            var coach = await context.Users
                .Where(user =>  user.Id == userId)
                .Include(coach => coach.Wrestlers)!
                .ThenInclude(wrestler => wrestler.WrestlingStyle)
                .FirstOrDefaultAsync();           

            if (coach == null)
                return null;

            var isCoach = await userManager.IsInRoleAsync(coach, UserRoles.Coach);

            if (!isCoach)
                return null;

            return coach;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();

            return roles;
        }
    }
}
