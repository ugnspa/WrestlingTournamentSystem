using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWeightCategoryRepository
    {
        public Task<bool> WeightCategoryExistsAsync(int weigthCategoryId);
        public Task<IEnumerable<WeightCategory>> GetWeightCategoriesAsync();
    }
}
