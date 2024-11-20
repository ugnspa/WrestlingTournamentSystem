namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWeightCategoryRepository
    {
        public Task<bool> WeightCategoryExistsAsync(int WeigthCategoryId);
    }
}
