using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWrestlingStyleRepository
    {
        public Task<bool> WrestlingStyleExistsAsync(int wrestlingStyleId);
        public Task<WrestlingStyle?> GetWrestlingStyleByIdAsync(int wrestlingStyleId);
        public Task<IEnumerable<WrestlingStyle>> GetWrestlingStylesAsync();
    }
}
