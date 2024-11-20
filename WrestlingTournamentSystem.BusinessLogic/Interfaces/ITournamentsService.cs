using WrestlingTournamentSystem.DataAccess.DTO.Tournament;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ITournamentsService
    {
        public Task<IEnumerable<TournamentReadDTO>> GetTournamentsAsync();
        public Task<TournamentReadDTO> GetTournamentAsync(int id);
        public Task<TournamentReadDTO> CreateTournamentAsync(string userId, TournamentCreateDTO tournamentCreateDTO);
        public Task<TournamentReadDTO> UpdateTournamentAsync(bool isAdmin, string userId, int tournamentId, TournamentUpdateDTO tournamentUpdateDTO);
        public Task DeleteTournamentAsync(bool isAdmin, string userId, int id);
    }
}
