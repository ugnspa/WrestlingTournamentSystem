using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface IWrestlerService
    {
        public Task<IEnumerable<WrestlerReadDTO>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId);
        public Task<WrestlerReadDTO?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId);
        public Task<WrestlerReadDTO?> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int tournamentWeightCategoryId, WrestlerCreateDTO wrestlerCreateDTO);
        public Task DeleteWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId);
    }
}
