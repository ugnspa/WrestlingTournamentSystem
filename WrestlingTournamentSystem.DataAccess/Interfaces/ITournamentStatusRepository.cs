using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentStatusRepository
    {
        public Task<TournamentStatus?> GetClosedTournamentStatus();
        public Task<TournamentStatus?> GetTournamentStatusById(int statusId);
    }
}
