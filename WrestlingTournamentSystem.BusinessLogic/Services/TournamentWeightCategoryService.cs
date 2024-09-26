using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using AutoMapper;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentWeightCategoryService : ITournamentWeightCategoryService
    {

        private readonly ITournamentWeightCategoryRepository _tournamentWeightCategoryRepository;
        private readonly ITournamentRepository _tournamentsRepository;
        private readonly IMapper _mapper;

        public TournamentWeightCategoryService(ITournamentWeightCategoryRepository tournamentWeightCategoryRepository, ITournamentRepository tournamentsRepository, IMapper mapper)
        {
            _tournamentWeightCategoryRepository = tournamentWeightCategoryRepository;
            _tournamentsRepository = tournamentsRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TournamentWeightCategoryReadDTO>> GetTournamentWeightCategoriesAsync(int tournamentId)
        {
            var tournamentExists = await _tournamentsRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
            {
                throw new ArgumentException($"Tournament with id {tournamentId} does not exist");
            }

            var tournamentWeightCategories = await _tournamentWeightCategoryRepository.GetTournamentWeightCategoriesAsync(tournamentId);

            return _mapper.Map<IEnumerable<TournamentWeightCategoryReadDTO>>(tournamentWeightCategories);
        }

        public async Task<TournamentWeightCategoryReadDTO> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            var tournamentExists = await _tournamentsRepository.TournamentExistsAsync(tournamentId);

            if (!tournamentExists)
            {
                throw new ArgumentException($"Tournament with id {tournamentId} does not exist");
            }

            var tournamentWeightCategory = await _tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);

            if (tournamentWeightCategory == null)
            {
                throw new ArgumentException($"Tournament weight category with id {tournamentWeightCategoryId} does not exist");
            }

            return _mapper.Map<TournamentWeightCategoryReadDTO>(tournamentWeightCategory);
        }

    }
}
