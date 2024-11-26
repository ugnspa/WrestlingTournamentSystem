using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WeightCategoryRepository(WrestlingTournamentSystemDbContext context) : IWeightCategoryRepository
    {
        public async Task<bool> WeightCategoryExistsAsync(int weigthCategoryId)
        {
            return await context.WeightCategories.AnyAsync(wc => wc.Id == weigthCategoryId);
        }
    }
}
