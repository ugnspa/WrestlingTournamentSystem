using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentWeightCategoryRepository
    {

        public Task<IEnumerable<TournamentWeightCategory>> GetTournamentWeightCategoriesAsync(int tournamentId);
        public Task<TournamentWeightCategory?> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task DeleteTournamentWeightCategoryAsync(TournamentWeightCategory tournamentWeightCategory);
        public Task<TournamentWeightCategory?> CreateTournamentWeightCategoryAsync(TournamentWeightCategory tournamentWeightCategory);
    }
}
