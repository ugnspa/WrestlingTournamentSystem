using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentRepository
    {
        public Task<IEnumerable<Tournament>> GetTournamentsAsync();
        public Task<Tournament?> GetTournamentAsync(int id);
        public Task<Tournament?> CreateTournamentAsync(Tournament tournament);
        public Task<Tournament?> UpdateTournamentAsync(Tournament tournament);
        public Task DeleteTournamentAsync(Tournament tournament);
    }
}
