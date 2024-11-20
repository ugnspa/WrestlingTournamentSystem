using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentStatusRepository : ITournamentStatusRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public TournamentStatusRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<TournamentStatus?> GetClosedTournamentStatus()
        {
            return await _context.TournamentStatuses.FirstOrDefaultAsync(ts => ts.Name == "Closed");
        }

        public async Task<TournamentStatus?> GetTournamentStatusById(int statusId)
        {
            return await _context.TournamentStatuses.FirstOrDefaultAsync(ts => ts.Id == statusId);
        }

        public async Task<bool> TournamentStatusExists(int statusId)
        {
            return await _context.TournamentStatuses.AnyAsync(ts => ts.Id == statusId);
        }
    }
}
