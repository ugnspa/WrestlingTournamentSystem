using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ITournamentWeightCategoryService
    {
        public Task<IEnumerable<TournamentWeightCategoryReadDTO>> GetTournamentWeightCategoriesAsync(int tournamentId);
        public Task<TournamentWeightCategoryReadDTO> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task DeleteTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task<TournamentWeightCategoryReadDTO> CreateTournamentWeightCategoryAsync(int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO);
        public Task<TournamentWeightCategoryReadDTO> UpdateTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId, TournamentWeightCategoryUpdateDTO tournamentWeightCategoryUpdateDTO);
    }
}
