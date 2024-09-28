using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class WrestlerService : IWrestlerService
    {
        private readonly IWrestlerRepository _wrestlerRepository;
        private readonly IWrestlingStyleRepository _wrestlingStyleRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentWeightCategoryRepository _tournamentWeightCategoryRepository;
        private readonly IMapper _mapper;
        public WrestlerService(IWrestlerRepository wrestlerRepository, IWrestlingStyleRepository wrestlingStyleRepository, ITournamentRepository tournamentRepository, ITournamentWeightCategoryRepository tournamentWeightCategoryRepository, IMapper mapper)
        {
            _wrestlerRepository = wrestlerRepository;
            _wrestlingStyleRepository = wrestlingStyleRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentWeightCategoryRepository = tournamentWeightCategoryRepository;
            _mapper = mapper;
        }

        private async Task ValidateTournamentAndWeightCategory(int tournamentId, int tournamentWeightCategoryId)
        {
            var tournamentExists = await _tournamentRepository.TournamentExistsAsync(tournamentId);
            if (!tournamentExists)
            {
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");
            }

            var tournamentWeightCategory = await _tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);
            if (tournamentWeightCategory == null)
            {
                throw new NotFoundException($"Tournament does not have weight category with id {tournamentWeightCategoryId}");
            }
        }

        public async Task<WrestlerReadDTO?> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int tournamentWeightCategoryId, WrestlerCreateDTO wrestlerCreateDTO)
        {
            if(wrestlerCreateDTO == null)
            {
                throw new ArgumentNullException(nameof(wrestlerCreateDTO));
            }

            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId);

            var wrestlingStyleExists = await _wrestlingStyleRepository.WrestlingStyleExistsAsync(wrestlerCreateDTO.StyleId);

            if (!wrestlingStyleExists)
            {
                throw new NotFoundException($"Wrestling style with id {wrestlerCreateDTO.StyleId} does not exist");
            }

            var wrestler = _mapper.Map<Wrestler>(wrestlerCreateDTO);
            //wrestler.PhotoUrl = AzureBlobService.DefaultWrestlerPhotoUrl; //Add default photo 

            var result = await _wrestlerRepository.CreateAndAddWrestlerToTournamentWeightCategory(tournamentId, tournamentWeightCategoryId, wrestler);

            if (result == null)
            {
                throw new Exception($"Failed to add wrestler to tournament weight category");
            }

            return _mapper.Map<WrestlerReadDTO>(result);
        }

        public async Task<WrestlerReadDTO?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId);

            var tournamentWeightCategoryWrestler = await _wrestlerRepository.GetTournamentWeightCategoryWrestlerAsync(tournamentId, tournamentWeightCategoryId, wrestlerId);

            if (tournamentWeightCategoryWrestler == null)
            {
                throw new NotFoundException($"Tournament weight category does not have wrestler with id {wrestlerId}");
            }

            return _mapper.Map<WrestlerReadDTO>(tournamentWeightCategoryWrestler);
        }

        public async Task<IEnumerable<WrestlerReadDTO>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId);

            return _mapper.Map<IEnumerable<WrestlerReadDTO>>(await _wrestlerRepository.GetTournamentWeightCategoryWrestlersAsync(tournamentId, tournamentWeightCategoryId));
        }
    }
}
