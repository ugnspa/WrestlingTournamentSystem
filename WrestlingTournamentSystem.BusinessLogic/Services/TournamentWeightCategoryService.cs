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

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentWeightCategoryService : ITournamentWeightCategoryService
    {

        private readonly ITournamentWeightCategoryRepository _tournamentWeightCategoryRepository;
        private readonly ITournamentRepository _tournamentsRepository;
        private readonly ITournamentWeightCategoryStatusRepository _tournamentWeightCategoryStatusRepository;
        private readonly IWeightCategoryRepository _weightCategoryRepository;
        private readonly IMapper _mapper;

        public TournamentWeightCategoryService(ITournamentWeightCategoryRepository tournamentWeightCategoryRepository, ITournamentRepository tournamentsRepository, ITournamentWeightCategoryStatusRepository tournamentWeightCategoryStatusRepository, IWeightCategoryRepository weightCategoryRepository, IMapper mapper)
        {
            _tournamentWeightCategoryRepository = tournamentWeightCategoryRepository;
            _tournamentsRepository = tournamentsRepository;
            _tournamentWeightCategoryStatusRepository = tournamentWeightCategoryStatusRepository;
            _weightCategoryRepository = weightCategoryRepository;
            _mapper = mapper;

        }

        public async Task<TournamentWeightCategoryReadDTO> CreateTournamentWeightCategoryAsync(int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO)
        {
            if(tournamentWeightCategoryCreateDTO == null)
                throw new ArgumentNullException(nameof(tournamentWeightCategoryCreateDTO));

            var tournamentExists = await _tournamentsRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            var tournamentWeightCategoryStatusExists = await _tournamentWeightCategoryStatusRepository.TournamentWeightCategoryStatusExistsAsync(tournamentWeightCategoryCreateDTO.StatusId);

            if(!tournamentWeightCategoryStatusExists)
                throw new NotFoundException($"Tournament weight category status with id {tournamentWeightCategoryCreateDTO.StatusId} does not exist");

            var weightCategoryExists = await _weightCategoryRepository.WeightCategoryExistsAsync(tournamentWeightCategoryCreateDTO.fk_WeightCategoryId);

            if(!weightCategoryExists)
                throw new NotFoundException($"Weight category with id {tournamentWeightCategoryCreateDTO.fk_WeightCategoryId} does not exist");
            
            var tournamentWeightCategory = _mapper.Map<TournamentWeightCategory>(tournamentWeightCategoryCreateDTO);
            tournamentWeightCategory.fk_TournamentId = tournamentId;

            var result = await _tournamentWeightCategoryRepository.CreateTournamentWeightCategoryAsync(tournamentWeightCategory);

            if (result == null)
                throw new Exception("Failed to create tournament weight category");

            return _mapper.Map<TournamentWeightCategoryReadDTO>(result);        
        }

        public async Task DeleteTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            var tournamentExists = await _tournamentsRepository.TournamentExistsAsync(tournamentId);
        
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
            var tournamentExists = await _tournamentsRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
            {
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");
            }

            var tournamentWeightCategories = await _tournamentWeightCategoryRepository.GetTournamentWeightCategoriesAsync(tournamentId);

            return _mapper.Map<IEnumerable<TournamentWeightCategoryReadDTO>>(tournamentWeightCategories);
        }

        public async Task<TournamentWeightCategoryReadDTO> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            var tournamentExists = await _tournamentsRepository.TournamentExistsAsync(tournamentId);

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

    }
}
