using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WrestlingStyleRepository(WrestlingTournamentSystemDbContext context) : IWrestlingStyleRepository
    {
        public async Task<WrestlingStyle?> GetWrestlingStyleByIdAsync(int wrestlingStyleId)
        {
            return await context.WrestlingStyles.FirstOrDefaultAsync(ws => ws.Id == wrestlingStyleId);
        }

        public async Task<IEnumerable<WrestlingStyle>> GetWrestlingStylesAsync()
        {
            return await context.WrestlingStyles.ToListAsync();
        }

        public async Task<bool> WrestlingStyleExistsAsync(int wrestlingStyleId)
        {
            return await context.WrestlingStyles.AnyAsync(ws => ws.Id == wrestlingStyleId);
        }
    }
}
