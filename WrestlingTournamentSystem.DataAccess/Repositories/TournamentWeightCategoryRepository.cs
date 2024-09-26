using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentWeightCategoryRepository : ITournamentWeightCategoryRepository
    {

        private readonly WrestlingTournamentSystemDbContext _context;

        public TournamentWeightCategoryRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TournamentWeightCategory>> GetTournamentWeightCategoriesAsync(int tournamentId)
        {
            return await _context.TournamentWeightCategories
                .Where(twc => twc.fk_TournamentId == tournamentId)
                .Include(twc => twc.WeightCategory)
                .Include(twc => twc.WeightCategory.WrestlingStyle)
                .Include(twc => twc.TournamentWeightCategoryStatus)
                .ToListAsync();
        }

        public async Task<TournamentWeightCategory?> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            return await _context.TournamentWeightCategories
                .Where(twc => twc.fk_TournamentId == tournamentId && twc.Id == tournamentWeightCategoryId)
                .Include(twc => twc.WeightCategory)
                .Include(twc => twc.WeightCategory.WrestlingStyle)
                .Include(twc => twc.TournamentWeightCategoryStatus)
                .FirstOrDefaultAsync();
        }
    }
}
