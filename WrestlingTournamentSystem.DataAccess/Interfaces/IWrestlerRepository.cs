using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWrestlerRepository
    {
        public Task<IEnumerable<Wrestler>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task<Wrestler?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId);
        public Task<Wrestler?> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int tournamentWeightCategoryId, Wrestler wrestler);
    }
}
