using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WrestlingStyleRepository(WrestlingTournamentSystemDbContext context) : IWrestlingStyleRepository
    {
        public Task<WrestlingStyle?> GetWrestlingStyleByIdAsync(int wrestlingStyleId)
        {
            return context.WrestlingStyles.FirstOrDefaultAsync(ws => ws.Id == wrestlingStyleId);
        }

        public Task<bool> WrestlingStyleExistsAsync(int wrestlingStyleId)
        {
            return context.WrestlingStyles.AnyAsync(ws => ws.Id == wrestlingStyleId);
        }
    }
}
