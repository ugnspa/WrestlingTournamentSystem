using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentWeightCategoryStatusRepository : ITournamentWeightCategoryStatusRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public TournamentWeightCategoryStatusRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TournamentWeightCategoryStatusExistsAsync(int tournamentWeightCategoryStatusId)
        {
            return await _context.TournamentWeightCategoryStatuses.AnyAsync(twcs => twcs.Id == tournamentWeightCategoryStatusId);
        }
    }
}
