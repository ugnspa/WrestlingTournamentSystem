using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentStatusRepository(WrestlingTournamentSystemDbContext context) : ITournamentStatusRepository
    {
        public async Task<TournamentStatus?> GetClosedTournamentStatus()
        {
            return await context.TournamentStatuses.FirstOrDefaultAsync(ts => ts.Name == "Closed");
        }

        public async Task<TournamentStatus?> GetTournamentStatusById(int statusId)
        {
            return await context.TournamentStatuses.FirstOrDefaultAsync(ts => ts.Id == statusId);
        }

        public async Task<bool> TournamentStatusExists(int statusId)
        {
            return await context.TournamentStatuses.AnyAsync(ts => ts.Id == statusId);
        }

        public async Task<IEnumerable<TournamentStatus>> GetTournamentStatusesAsync()
        {
            return await context.TournamentStatuses.ToListAsync();
        }
    }
}
