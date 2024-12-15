using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WrestlerRepository(WrestlingTournamentSystemDbContext context) : IWrestlerRepository
    {
        public async Task<Wrestler?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            return await context.TournamentWeightCategories
                 .Where(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId)
                 .SelectMany(twc => twc.Wrestlers!)
                 .Include(w => w.WrestlingStyle)
                 .Include(w => w.Coach)
                 .FirstOrDefaultAsync(w => w.Id == wrestlerId);
        }

        public async Task<IEnumerable<Wrestler>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            return await context.TournamentWeightCategories
                .Where(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId)
                .SelectMany(twc => twc.Wrestlers!)
                .Include(w => w.WrestlingStyle)
                .Include(w => w.Coach)
                .ToListAsync();
        }

        public async Task<Wrestler?> CreateAndAddWrestlerToTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId, Wrestler wrestler)
        {
            var tournamentWeightCategory = await context.TournamentWeightCategories
                .Include(twc => twc.Wrestlers)
                .FirstOrDefaultAsync(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId);

            if (tournamentWeightCategory == null)
            {
                return null;
            }

            tournamentWeightCategory.Wrestlers!.Add(wrestler);

            await context.SaveChangesAsync();

            return await GetWrestlerByIdAsync(wrestler.Id);
        }

        public async Task DeleteWrestlerAsync(Wrestler wrestler)
        {
            context.Wrestlers.Remove(wrestler);
            await context.SaveChangesAsync();
        }

        public async Task<Wrestler?> UpdateWrestlerAsync(Wrestler wrestler)
        {
            var wrestlerToUpdate = await context.Wrestlers.FindAsync(wrestler.Id);

            if (wrestlerToUpdate == null)
                throw new NotFoundException($"Wrestler with id {wrestler.Id} was not found");

            context.Entry(wrestlerToUpdate).CurrentValues.SetValues(wrestler);

            await context.SaveChangesAsync();

            return await GetWrestlerByIdAsync(wrestlerToUpdate.Id);
        }

        public async Task<Wrestler?> GetWrestlerByIdAsync(int wrestlerId)
        {
            return await context.Wrestlers
                .Include(w => w.WrestlingStyle)
                .Include(w => w.Coach)
                .FirstOrDefaultAsync(w => w.Id == wrestlerId);
        }

        public async Task<IEnumerable<Wrestler>> GetAllWrestlersAsync()
        {
            return await context.Wrestlers
                .Include(w => w.WrestlingStyle)
                .Include(w => w.Coach)
                .ToListAsync();
        }
    }
}
