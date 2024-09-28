using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using AutoMapper;
using WrestlingTournamentSystem.DataAccess.Exceptions;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Data;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentWeightCategoryService : ITournamentWeightCategoryService
    {

        private readonly ITournamentWeightCategoryRepository _tournamentWeightCategoryRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentWeightCategoryStatusRepository _tournamentWeightCategoryStatusRepository;
        private readonly IWeightCategoryRepository _weightCategoryRepository;
        private readonly IMapper _mapper;

        public TournamentWeightCategoryService(ITournamentWeightCategoryRepository tournamentWeightCategoryRepository, ITournamentRepository tournamentRepository, ITournamentWeightCategoryStatusRepository tournamentWeightCategoryStatusRepository, IWeightCategoryRepository weightCategoryRepository, IMapper mapper)
        {
            _tournamentWeightCategoryRepository = tournamentWeightCategoryRepository;
            _tournamentRepository = tournamentRepository;
            _tournamentWeightCategoryStatusRepository = tournamentWeightCategoryStatusRepository;
            _weightCategoryRepository = weightCategoryRepository;
            _mapper = mapper;

        }

        public async Task<TournamentWeightCategoryReadDTO> CreateTournamentWeightCategoryAsync(int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO)
        {
            if(tournamentWeightCategoryCreateDTO == null)
                throw new ArgumentNullException(nameof(tournamentWeightCategoryCreateDTO));

            var tournamentExists = await _tournamentRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            var weightCategoryExists = await _weightCategoryRepository.WeightCategoryExistsAsync(tournamentWeightCategoryCreateDTO.fk_WeightCategoryId);

            if(!weightCategoryExists)
                throw new NotFoundException($"Weight category with id {tournamentWeightCategoryCreateDTO.fk_WeightCategoryId} does not exist");

            var closedWeightCategoryStatus = await _tournamentWeightCategoryStatusRepository.GetClosedTournamentWeightCategoryStatus();

            if (closedWeightCategoryStatus == null)
                throw new NotFoundException("Failed to get closed tournament weight category status");

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

            var tournamentExists = await _tournamentRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
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

            _mapper.Map(tournamentWeightCategoryUpdateDTO, tournamentWeightCategoryToUpdate);

            var result = await _tournamentWeightCategoryRepository.UpdateTournamentWeightCategoryAsync(tournamentWeightCategoryToUpdate);

            if (result == null)
                throw new Exception("Failed to update tournament weight category");

            return _mapper.Map<TournamentWeightCategoryReadDTO>(result);
        }
    }
}
