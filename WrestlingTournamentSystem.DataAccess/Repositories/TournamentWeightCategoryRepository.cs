using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class TournamentWeightCategoryRepository : ITournamentWeightCategoryRepository
    {

        private readonly WrestlingTournamentSystemDbContext _context;

        public TournamentWeightCategoryRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<TournamentWeightCategory?> CreateTournamentWeightCategoryAsync(TournamentWeightCategory tournamentWeightCategory)
        {
            _context.TournamentWeightCategories.Add(tournamentWeightCategory);
            await _context.SaveChangesAsync();

            return await GetTournamentWeightCategoryAsync(tournamentWeightCategory.fk_TournamentId, tournamentWeightCategory.Id);
        }

        public async Task DeleteTournamentWeightCategoryAsync(TournamentWeightCategory tournamentWeightCategory)
        {
            _context.TournamentWeightCategories.Remove(tournamentWeightCategory);
            await _context.SaveChangesAsync();
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

        public async Task<TournamentWeightCategory?> UpdateTournamentWeightCategoryAsync(TournamentWeightCategory tournamentWeightCategory)
        {
            var tournamentWeightCategoryToUpdate = _context.TournamentWeightCategories.Find(tournamentWeightCategory.Id);

            if (tournamentWeightCategoryToUpdate == null)
                throw new NotFoundException($"Tournament weight category with id {tournamentWeightCategory.Id} was not found");

            _context.Entry(tournamentWeightCategoryToUpdate).CurrentValues.SetValues(tournamentWeightCategory);

            await _context.SaveChangesAsync();

            return await GetTournamentWeightCategoryAsync(tournamentWeightCategoryToUpdate.fk_TournamentId, tournamentWeightCategoryToUpdate.Id);
        }
    }
}
