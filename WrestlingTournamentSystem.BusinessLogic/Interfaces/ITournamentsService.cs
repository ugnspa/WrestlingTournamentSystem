using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ITournamentsService
    {
        public Task<IEnumerable<TournamentReadDTO>> GetTournamentsAsync();
        public Task<TournamentReadDTO> GetTournamentAsync(int id);
        public Task<TournamentReadDTO> CreateTournamentAsync(TournamentCreateDTO tournamentCreateDTO);
        public Task<TournamentReadDTO> UpdateTournamentAsync(int tournamentId, TournamentUpdateDTO tournamentUpdateDTO);
        public Task DeleteTournamentAsync(int id);
    }
}
