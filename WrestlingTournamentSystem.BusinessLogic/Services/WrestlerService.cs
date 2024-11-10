using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
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
        private readonly IValidationService _validationService;
        public WrestlerService(IWrestlerRepository wrestlerRepository, IWrestlingStyleRepository wrestlingStyleRepository, ITournamentRepository tournamentRepository, ITournamentWeightCategoryRepository tournamentWeightCategoryRepository, IMapper mapper, IValidationService validationService)
        {
            _wrestlerRepository = wrestlerRepository;
            _wrestlingStyleRepository = wrestlingStyleRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentWeightCategoryRepository = tournamentWeightCategoryRepository;
            _mapper = mapper;
            _validationService = validationService;
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

        private void ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate > DateTime.Now)
            {
                throw new BusinessRuleValidationException("Birth date cannot be in the future");
            }

            if (birthDate < new DateTime(1900, 1, 1))
            {
                throw new BusinessRuleValidationException("Birth date cannot be earlier than January 1, 1900.");
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

            _validationService.ValidateBirthDate(wrestlerCreateDTO.BirthDate);

            var wrestler = _mapper.Map<Wrestler>(wrestlerCreateDTO);
            //wrestler.PhotoUrl = AzureBlobService.DefaultWrestlerPhotoUrl; //Add default photo 

            var result = await _wrestlerRepository.CreateAndAddWrestlerToTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId, wrestler);

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

        public async Task DeleteWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId);

            var wrestler = await _wrestlerRepository.GetTournamentWeightCategoryWrestlerAsync(tournamentId, tournamentWeightCategoryId, wrestlerId);
            
            if (wrestler == null)
            {
                throw new NotFoundException($"Tournament weight category does not have wrestler with id {wrestlerId}");
            }

            await _wrestlerRepository.DeleteWrestlerAsync(wrestler); 
        }

        public async Task<WrestlerReadDTO?> UpdateWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId, WrestlerUpdateDTO wrestlerUpdateDTO)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId);

            var tounamentWeightCategoryWrestler = await _wrestlerRepository.GetTournamentWeightCategoryWrestlerAsync(tournamentId, tournamentWeightCategoryId, wrestlerId);

            if (tounamentWeightCategoryWrestler == null)
                throw new NotFoundException($"Tournament weight category does not have wrestler with id {wrestlerId}");
            
            var wrestlingStyleExists = await _wrestlingStyleRepository.WrestlingStyleExistsAsync(wrestlerUpdateDTO.StyleId);

            if (!wrestlingStyleExists)
                throw new NotFoundException($"Wrestling style with id {wrestlerUpdateDTO.StyleId} does not exist");

            _validationService.ValidateBirthDate(wrestlerUpdateDTO.BirthDate);

            _mapper.Map(wrestlerUpdateDTO, tounamentWeightCategoryWrestler);

            var result = await _wrestlerRepository.UpdateWrestlerAsync(tounamentWeightCategoryWrestler);

            if (result == null)
                throw new Exception($"Failed to update wrestler with id {wrestlerId}");

            return _mapper.Map<WrestlerReadDTO>(result);
        }
    }
}
