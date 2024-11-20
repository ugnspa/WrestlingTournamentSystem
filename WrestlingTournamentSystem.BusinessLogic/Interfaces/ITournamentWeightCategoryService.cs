using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ITournamentWeightCategoryService
    {
        public Task<IEnumerable<TournamentWeightCategoryReadDTO>> GetTournamentWeightCategoriesAsync(int tournamentId);
        public Task<TournamentWeightCategoryReadDTO> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task DeleteTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId);
        public Task<TournamentWeightCategoryReadDTO> CreateTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO);
        public Task<TournamentWeightCategoryReadDTO> UpdateTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, TournamentWeightCategoryUpdateDTO tournamentWeightCategoryUpdateDTO);
    }
}
