using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWrestlerRepository
    {
        public Task<IEnumerable<Wrestler>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task<Wrestler?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId);
        public Task<Wrestler?> CreateAndAddWrestlerToTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId, Wrestler wrestler);
        public Task<Wrestler?> UpdateWrestlerAsync(Wrestler wrestler);
        public Task DeleteWrestlerAsync(Wrestler wrestler);
        public Task RemoveWrestlerFromTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId, Wrestler wrestler);
        public Task<Wrestler?> GetWrestlerByIdAsync(int wrestlerId);
        public Task<IEnumerable<Wrestler>> GetAllWrestlersAsync();
    }
}
