using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface IWrestlingStyleRepository
    {
        public Task<bool> WrestlingStyleExistsAsync(int wrestlingStyleId);
        public Task<WrestlingStyle?> GetWrestlingStyleByIdAsync(int wrestlingStyleId);
    }
}
