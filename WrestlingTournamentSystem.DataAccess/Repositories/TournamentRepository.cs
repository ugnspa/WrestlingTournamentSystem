using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Data
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public TournamentRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Tournament?> CreateTournamentAsync(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return await GetTournamentAsync(tournament.Id);
        }

        public async Task DeleteTournamentAsync(Tournament tournament)
        {

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task<Tournament?> GetTournamentAsync(int id)
        {
            return await _context.Tournaments.Include(t => t.TournamentStatus).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsAsync()
        {
            return await _context.Tournaments.Include(t => t.TournamentStatus).ToListAsync();
        }

        public async Task<Tournament?> UpdateTournamentAsync(Tournament tournament)
        {
            var tournamentToUpdate = _context.Tournaments.Find(tournament.Id);

            if (tournamentToUpdate == null)
                throw new NotFoundException($"Tournament with id {tournament.Id} was not found");

            _context.Entry(tournamentToUpdate).CurrentValues.SetValues(tournament);

            await _context.SaveChangesAsync();

            return await GetTournamentAsync(tournament.Id);
        }


    }
}
