using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        public Task CreateUser(User user, string password);
        public Task<User?> FindByUsernameAsync(string userName);
    }
}