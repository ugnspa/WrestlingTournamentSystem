using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentWeightCategoryStatusRepository
    {
        public Task<bool> TournamentWeightCategoryStatusExistsAsync(int tournamentWeightCategoryStatusId);
        public Task<TournamentWeightCategoryStatus?> GetClosedTournamentWeightCategoryStatus();
    }
}
