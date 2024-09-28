using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WrestlerRepository : IWrestlerRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public WrestlerRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Wrestler?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            return await _context.TournamentWeightCategories
                 .Where(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId)
                 .SelectMany(twc => twc.Wrestlers)
                 .FirstOrDefaultAsync(w => w.Id == wrestlerId);
        }

        public async Task<IEnumerable<Wrestler>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            return await _context.TournamentWeightCategories
                .Where(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId)
                .SelectMany(twc => twc.Wrestlers)
                .ToListAsync();
        }
    }
}
