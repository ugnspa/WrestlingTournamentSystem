using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentWeightCategoryStatusRepository(WrestlingTournamentSystemDbContext context)
        : ITournamentWeightCategoryStatusRepository
    {
        public async Task<bool> TournamentWeightCategoryStatusExistsAsync(int tournamentWeightCategoryStatusId)
        {
            return await context.TournamentWeightCategoryStatuses.AnyAsync(twcs => twcs.Id == tournamentWeightCategoryStatusId);
        }

        public async Task<TournamentWeightCategoryStatus?> GetClosedTournamentWeightCategoryStatus()
        {
            return await context.TournamentWeightCategoryStatuses.FirstOrDefaultAsync(twcs => twcs.Name == "Closed");
        } 

        public async Task<IEnumerable<TournamentWeightCategoryStatus>> GetTournamentWeightCategoryStatusesAsync()
        {
            return await context.TournamentWeightCategoryStatuses.ToListAsync();
        }
    }
}
