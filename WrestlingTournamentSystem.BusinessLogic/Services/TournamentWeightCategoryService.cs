using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using AutoMapper;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.BusinessLogic.Validation;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentWeightCategoryService : ITournamentWeightCategoryService
    {

        private readonly ITournamentWeightCategoryRepository _tournamentWeightCategoryRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentWeightCategoryStatusRepository _tournamentWeightCategoryStatusRepository;
        private readonly IWeightCategoryRepository _weightCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public TournamentWeightCategoryService(ITournamentWeightCategoryRepository tournamentWeightCategoryRepository, ITournamentRepository tournamentRepository, ITournamentWeightCategoryStatusRepository tournamentWeightCategoryStatusRepository, IWeightCategoryRepository weightCategoryRepository, IMapper mapper, IValidationService validationService)
        {
            _tournamentWeightCategoryRepository = tournamentWeightCategoryRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentWeightCategoryStatusRepository = tournamentWeightCategoryStatusRepository;
            _weightCategoryRepository = weightCategoryRepository;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<TournamentWeightCategoryReadDTO> CreateTournamentWeightCategoryAsync(int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO)
        {
            if(tournamentWeightCategoryCreateDTO == null)
                throw new ArgumentNullException(nameof(tournamentWeightCategoryCreateDTO));

            var tournament = await _tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            var weightCategoryExists = await _weightCategoryRepository.WeightCategoryExistsAsync(tournamentWeightCategoryCreateDTO.fk_WeightCategoryId);

            if(!weightCategoryExists)
                throw new NotFoundException($"Weight category with id {tournamentWeightCategoryCreateDTO.fk_WeightCategoryId} does not exist");

            var closedWeightCategoryStatus = await _tournamentWeightCategoryStatusRepository.GetClosedTournamentWeightCategoryStatus();

            if (closedWeightCategoryStatus == null)
                throw new NotFoundException("Failed to get closed tournament weight category status");

            _validationService.ValidateTournamentWeightCategoryDates(tournament.StartDate, tournament.EndDate, tournamentWeightCategoryCreateDTO.StartDate, tournamentWeightCategoryCreateDTO.EndDate);

            var tournamentWeightCategory = _mapper.Map<TournamentWeightCategory>(tournamentWeightCategoryCreateDTO);

            tournamentWeightCategory.fk_TournamentId = tournamentId;
            tournamentWeightCategory.TournamentWeightCategoryStatus = closedWeightCategoryStatus;

            var result = await _tournamentWeightCategoryRepository.CreateTournamentWeightCategoryAsync(tournamentWeightCategory);

            if (result == null)
                throw new Exception("Failed to create tournament weight category");

            return _mapper.Map<TournamentWeightCategoryReadDTO>(result);        
        }

        public async Task DeleteTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
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

            await _tournamentWeightCategoryRepository.DeleteTournamentWeightCategoryAsync(tournamentWeightCategory);
        }

        public async Task<IEnumerable<TournamentWeightCategoryReadDTO>> GetTournamentWeightCategoriesAsync(int tournamentId)
        {
            var tournamentExists = await _tournamentRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
            {
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");
            }

            var tournamentWeightCategories = await _tournamentWeightCategoryRepository.GetTournamentWeightCategoriesAsync(tournamentId);

            return _mapper.Map<IEnumerable<TournamentWeightCategoryReadDTO>>(tournamentWeightCategories);
        }

        public async Task<TournamentWeightCategoryReadDTO> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
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

            return _mapper.Map<TournamentWeightCategoryReadDTO>(tournamentWeightCategory);
        }

        public async Task<TournamentWeightCategoryReadDTO> UpdateTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId, TournamentWeightCategoryUpdateDTO tournamentWeightCategoryUpdateDTO)
        {
            if (tournamentWeightCategoryUpdateDTO == null)
                throw new ArgumentNullException(nameof(tournamentWeightCategoryUpdateDTO));

            var tournament = await _tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            var tournamentWeightCategoryToUpdate = await _tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);

            if (tournamentWeightCategoryToUpdate == null)
                throw new NotFoundException($"Tournament does not have weight category with id {tournamentWeightCategoryId}");

            var tournamentWeightCategoryStatusExists = await _tournamentWeightCategoryStatusRepository.TournamentWeightCategoryStatusExistsAsync(tournamentWeightCategoryUpdateDTO.StatusId);

            if(!tournamentWeightCategoryStatusExists)
                throw new NotFoundException($"Tournament weight category status with id {tournamentWeightCategoryUpdateDTO.StatusId} does not exist");

            var weightCategoryExists = await _weightCategoryRepository.WeightCategoryExistsAsync(tournamentWeightCategoryUpdateDTO.fk_WeightCategoryId);

            if (!weightCategoryExists)
                throw new NotFoundException($"Weight category with id {tournamentWeightCategoryUpdateDTO.fk_WeightCategoryId} does not exist");

            _validationService.ValidateTournamentWeightCategoryDates(tournament.StartDate, tournament.EndDate, tournamentWeightCategoryUpdateDTO.StartDate, tournamentWeightCategoryUpdateDTO.EndDate);

            _mapper.Map(tournamentWeightCategoryUpdateDTO, tournamentWeightCategoryToUpdate);

            var result = await _tournamentWeightCategoryRepository.UpdateTournamentWeightCategoryAsync(tournamentWeightCategoryToUpdate);

            if (result == null)
                throw new Exception("Failed to update tournament weight category");

            return _mapper.Map<TournamentWeightCategoryReadDTO>(result);
        }
    }
}
