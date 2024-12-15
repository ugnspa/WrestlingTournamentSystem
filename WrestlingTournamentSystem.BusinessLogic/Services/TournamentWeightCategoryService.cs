using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Interfaces;
using AutoMapper;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using WrestlingTournamentSystem.DataAccess.DTO.WeightCategory;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentWeightCategoryService(
        ITournamentWeightCategoryRepository tournamentWeightCategoryRepository,
        ITournamentRepository tournamentRepository,
        ITournamentWeightCategoryStatusRepository tournamentWeightCategoryStatusRepository,
        IWeightCategoryRepository weightCategoryRepository,
        IMapper mapper,
        IValidationService validationService)
        : ITournamentWeightCategoryService
    {
        public async Task<TournamentWeightCategoryReadDto> CreateTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, TournamentWeightCategoryCreateDto tournamentWeightCategoryCreateDto)
        {
            if(tournamentWeightCategoryCreateDto == null)
                throw new ArgumentNullException(nameof(tournamentWeightCategoryCreateDto));

            var tournament = await tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            if (tournament.OrganiserId != userId && !isAdmin)
                throw new ForbiddenException("You are not allowed to create tournament weight category for this tournament");

            var weightCategoryExists = await weightCategoryRepository.WeightCategoryExistsAsync(tournamentWeightCategoryCreateDto.fk_WeightCategoryId);

            if(!weightCategoryExists)
                throw new NotFoundException($"Weight category with id {tournamentWeightCategoryCreateDto.fk_WeightCategoryId} does not exist");

            var closedWeightCategoryStatus = await tournamentWeightCategoryStatusRepository.GetClosedTournamentWeightCategoryStatus();

            if (closedWeightCategoryStatus == null)
                throw new NotFoundException("Failed to get closed tournament weight category status");

            validationService.ValidateTournamentWeightCategoryDates(tournament.StartDate, tournament.EndDate, tournamentWeightCategoryCreateDto.StartDate, tournamentWeightCategoryCreateDto.EndDate);

            var tournamentWeightCategory = mapper.Map<TournamentWeightCategory>(tournamentWeightCategoryCreateDto);

            tournamentWeightCategory.fk_TournamentId = tournamentId;
            tournamentWeightCategory.TournamentWeightCategoryStatus = closedWeightCategoryStatus;

            var result = await tournamentWeightCategoryRepository.CreateTournamentWeightCategoryAsync(tournamentWeightCategory);

            if (result == null)
                throw new Exception("Failed to create tournament weight category");

            return mapper.Map<TournamentWeightCategoryReadDto>(result);        
        }

        public async Task DeleteTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId)
        {
            var tournament = await tournamentRepository.GetTournamentAsync(tournamentId);
        
            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            if (tournament.OrganiserId != userId && !isAdmin)
                throw new ForbiddenException("You are not allowed to delete tournament weight category for this tournament");

            var tournamentWeightCategory = await tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);

            if (tournamentWeightCategory == null)
                throw new NotFoundException($"Tournament does not have weight category with id {tournamentWeightCategoryId}");

            await tournamentWeightCategoryRepository.DeleteTournamentWeightCategoryAsync(tournamentWeightCategory);
        }

        public async Task<IEnumerable<TournamentWeightCategoryReadDto>> GetTournamentWeightCategoriesAsync(int tournamentId)
        {
            var tournament = await tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            var tournamentWeightCategories = await tournamentWeightCategoryRepository.GetTournamentWeightCategoriesAsync(tournamentId);

            return mapper.Map<IEnumerable<TournamentWeightCategoryReadDto>>(tournamentWeightCategories);
        }

        public async Task<TournamentWeightCategoryReadDto> GetTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            var tournament = await tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            var tournamentWeightCategory = await tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);

            if (tournamentWeightCategory == null)
                throw new NotFoundException($"Tournament does not have weight category with id {tournamentWeightCategoryId}");

            return mapper.Map<TournamentWeightCategoryReadDto>(tournamentWeightCategory);
        }

        public async Task<TournamentWeightCategoryReadDto> UpdateTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, TournamentWeightCategoryUpdateDto tournamentWeightCategoryUpdateDto)
        {
            if (tournamentWeightCategoryUpdateDto == null)
                throw new ArgumentNullException(nameof(tournamentWeightCategoryUpdateDto));

            var tournament = await tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            if (tournament.OrganiserId != userId && !isAdmin)
                throw new ForbiddenException("You are not allowed to update tournament weight category for this tournament");

            var tournamentWeightCategoryToUpdate = await tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);

            if (tournamentWeightCategoryToUpdate == null)
                throw new NotFoundException($"Tournament does not have weight category with id {tournamentWeightCategoryId}");

            var tournamentWeightCategoryStatusExists = await tournamentWeightCategoryStatusRepository.TournamentWeightCategoryStatusExistsAsync(tournamentWeightCategoryUpdateDto.StatusId);

            if(!tournamentWeightCategoryStatusExists)
                throw new NotFoundException($"Tournament weight category status with id {tournamentWeightCategoryUpdateDto.StatusId} does not exist");

            //validationService.ValidateTournamentWeightCategoryDates(tournament.StartDate, tournament.EndDate, tournamentWeightCategoryUpdateDto.StartDate, tournamentWeightCategoryUpdateDto.EndDate);

            mapper.Map(tournamentWeightCategoryUpdateDto, tournamentWeightCategoryToUpdate);

            var result = await tournamentWeightCategoryRepository.UpdateTournamentWeightCategoryAsync(tournamentWeightCategoryToUpdate);

            if (result == null)
                throw new Exception("Failed to update tournament weight category");

            return mapper.Map<TournamentWeightCategoryReadDto>(result);
        }

        public async Task<IEnumerable<TournamentWeightCategoryStatus>> GetTournamentWeightCategoryStatusesAsync()
        {
            var tournamentWeightCategoryStatuses = await tournamentWeightCategoryStatusRepository.GetTournamentWeightCategoryStatusesAsync();

            return tournamentWeightCategoryStatuses;
        }

        public async Task<IEnumerable<WeightCategoryReadDto>> GetWeightCategoriesAsync()
        {
            var weightCategories = await weightCategoryRepository.GetWeightCategoriesAsync();

            return mapper.Map<IEnumerable<WeightCategoryReadDto>>(weightCategories);
        }
    }
}
