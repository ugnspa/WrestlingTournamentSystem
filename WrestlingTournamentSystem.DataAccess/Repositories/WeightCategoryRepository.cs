using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WeightCategoryRepository : IWeightCategoryRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public WeightCategoryRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> WeightCategoryExistsAsync(int WeigthCategoryId)
        {
            return await _context.WeightCategories.AnyAsync(wc => wc.Id == WeigthCategoryId);
        }
    }
}
