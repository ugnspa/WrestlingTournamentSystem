using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentWeightCategoryStatusRepository
    {
        public Task<bool> TournamentWeightCategoryStatusExistsAsync(int tournamentWeightCategoryStatusId);
        public Task<TournamentWeightCategoryStatus?> GetClosedTournamentWeightCategoryStatus();
    }
}
