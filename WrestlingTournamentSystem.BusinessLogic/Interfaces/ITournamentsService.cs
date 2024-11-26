using WrestlingTournamentSystem.DataAccess.DTO.Tournament;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ITournamentsService
    {
        public Task<IEnumerable<TournamentReadDto>> GetTournamentsAsync();
        public Task<TournamentReadDto> GetTournamentAsync(int id);
        public Task<TournamentReadDto> CreateTournamentAsync(string userId, TournamentCreateDto tournamentCreateDto);
        public Task<TournamentReadDto> UpdateTournamentAsync(bool isAdmin, string userId, int tournamentId, TournamentUpdateDto tournamentUpdateDto);
        public Task DeleteTournamentAsync(bool isAdmin, string userId, int id);
    }
}
