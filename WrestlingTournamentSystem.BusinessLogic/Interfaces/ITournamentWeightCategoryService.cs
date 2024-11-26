using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ITournamentWeightCategoryService
    {
        public Task<IEnumerable<TournamentWeightCategoryReadDto>> GetTournamentWeightCategoriesAsync(int tournamentId);
        public Task<TournamentWeightCategoryReadDto> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task DeleteTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId);
        public Task<TournamentWeightCategoryReadDto> CreateTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, TournamentWeightCategoryCreateDto tournamentWeightCategoryCreateDto);
        public Task<TournamentWeightCategoryReadDto> UpdateTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, TournamentWeightCategoryUpdateDto tournamentWeightCategoryUpdateDto);
    }
}
