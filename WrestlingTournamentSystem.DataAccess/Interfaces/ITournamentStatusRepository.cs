using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentStatusRepository
    {
        public Task<TournamentStatus?> GetClosedTournamentStatus();
        public Task<TournamentStatus?> GetTournamentStatusById(int statusId);
        public Task<bool> TournamentStatusExists(int statusId);
    }
}
