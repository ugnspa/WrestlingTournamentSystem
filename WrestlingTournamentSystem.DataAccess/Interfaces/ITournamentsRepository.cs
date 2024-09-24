using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ITournamentsRepository
    {
        public Task<IEnumerable<Tournament>> GetTournamentsAsync();
        public Task<Tournament?> GetTournamentAsync(int id);
        public Task CreateTournamentAsync(Tournament tournament);
        public Task UpdateTournamentAsync(Tournament tournament);
        public Task DeleteTournamentAsync(Tournament tournament);
    }
}
