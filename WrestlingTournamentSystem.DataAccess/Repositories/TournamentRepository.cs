using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentRepository(WrestlingTournamentSystemDbContext context) : ITournamentRepository
    {
        public async Task<Tournament?> CreateTournamentAsync(Tournament tournament)
        {
            context.Tournaments.Add(tournament);
            await context.SaveChangesAsync();

            return await GetTournamentAsync(tournament.Id);
        }

        public async Task DeleteTournamentAsync(Tournament tournament)
        {

            context.Tournaments.Remove(tournament);
            await context.SaveChangesAsync();
        }

        public async Task<Tournament?> GetTournamentAsync(int id)
        {
            return await context.Tournaments.Include(t => t.TournamentStatus).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsAsync()
        {
            return await context.Tournaments.Include(t => t.TournamentStatus).ToListAsync();
        }

        public async Task<Tournament?> UpdateTournamentAsync(Tournament tournament)
        {
            var tournamentToUpdate = await context.Tournaments.FindAsync(tournament.Id) ?? throw new NotFoundException($"Tournament with id {tournament.Id} was not found");

            context.Entry(tournamentToUpdate).CurrentValues.SetValues(tournament);

            await context.SaveChangesAsync();

            return await GetTournamentAsync(tournament.Id);
        }


    }
}
