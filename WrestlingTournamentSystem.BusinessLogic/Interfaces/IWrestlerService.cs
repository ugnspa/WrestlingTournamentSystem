using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface IWrestlerService
    {
        public Task<IEnumerable<WrestlerReadDto>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task<WrestlerReadDto?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId);
        public Task<WrestlerReadDto?> CreateAndAddWrestlerToTournamentWeightCategory(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, WrestlerCreateDto wrestlerCreateDto);
        public Task<WrestlerReadDto?> UpdateWrestlerAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, int wrestlerId, WrestlerUpdateDto wrestlerUpdateDto);
        public Task RemoveWrestlerFromTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, int wrestlerId);
        public Task<WrestlerReadDto> GetWrestlerByIdAsync(int wrestlerId);
        public Task<IEnumerable<WrestlerReadDto>> GetAllWrestlersAsync();
        public Task<IEnumerable<WrestlingStyle>> GetWrestlingStylesAsync();
    }
}
