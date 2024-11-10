using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        public Task CreateUserAsync(User user, string password);
        public Task<User?> FindByUsernameAsync(string userName);
        public Task<User?> FindByIdAsync(string userId);
        public Task<bool> IsPasswordValidAsync(User user, string password);
        public Task<IList<string>> GetUserRolesAsync(User user);
        public Task<IEnumerable<User>> GetCoaches();
        public Task<User?> GetCoachWithWrestlersAsync(string userId);
    }
}