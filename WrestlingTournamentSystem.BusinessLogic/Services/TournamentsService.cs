using AutoMapper;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class TournamentsService(
        ITournamentRepository tournamentRepository,
        ITournamentStatusRepository tournamentStatusRepository,
        IMapper mapper,
        IValidationService validationService)
        : ITournamentsService
    {
        public async Task<TournamentReadDto> CreateTournamentAsync(string userId, TournamentCreateDto tournamentCreateDto)
        {
            if (tournamentCreateDto == null)
                throw new ArgumentNullException(nameof(tournamentCreateDto));

            if (String.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            validationService.ValidateStartEndDates(tournamentCreateDto.StartDate, tournamentCreateDto.EndDate);

            var tournament = mapper.Map<Tournament>(tournamentCreateDto);

            tournament.OrganiserId = userId;

            var closedStatus = await tournamentStatusRepository.GetClosedTournamentStatus();

            if (closedStatus == null)
                throw new NotFoundException("Closed status was not found");

            tournament.TournamentStatus = closedStatus;

            var result = await tournamentRepository.CreateTournamentAsync(tournament);

            if (result == null)
                throw new Exception("Failed to create tournament");

            return mapper.Map<TournamentReadDto>(result);
        }

        public async Task DeleteTournamentAsync(bool isAdmin, string userId, int id)
        {
            var tournament = await tournamentRepository.GetTournamentAsync(id);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {id} was not found");

            if (tournament.OrganiserId != userId && !isAdmin)
                throw new ForbiddenException("You are not allowed to delete this tournament");

            await tournamentRepository.DeleteTournamentAsync(tournament);
        }

        public async Task<TournamentReadDto> GetTournamentAsync(int id)
        {
            var tournament = await tournamentRepository.GetTournamentAsync(id);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {id} was not found");

            return mapper.Map<TournamentReadDto>(tournament);
        }

        public async Task<IEnumerable<TournamentReadDto>> GetTournamentsAsync()
        {
            var tournaments = await tournamentRepository.GetTournamentsAsync();

            return mapper.Map<IEnumerable<TournamentReadDto>>(tournaments);
        }

        public async Task<TournamentReadDto> UpdateTournamentAsync(bool isAdmin, string userId, int tournamentId, TournamentUpdateDto tournamentUpdateDto)
        {
            if (tournamentUpdateDto == null)
                throw new ArgumentNullException(nameof(tournamentUpdateDto));

            var tournamentStatusExists = await tournamentStatusRepository.TournamentStatusExists(tournamentUpdateDto.StatusId);

            if (!tournamentStatusExists)
                throw new NotFoundException($"Tournament status with id {tournamentUpdateDto.StatusId} was not found");

            var tournamentToUpdate = await tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournamentToUpdate == null)
                throw new NotFoundException($"Tournament with id {tournamentId} was not found");

            if (tournamentToUpdate.OrganiserId != userId && !isAdmin)
                throw new ForbiddenException("You are not allowed to update this tournament");

            validationService.ValidateStartEndDates(tournamentUpdateDto.StartDate, tournamentUpdateDto.EndDate);

            mapper.Map(tournamentUpdateDto, tournamentToUpdate);

            var result = await tournamentRepository.UpdateTournamentAsync(tournamentToUpdate);

            if (result == null)
                throw new Exception("Failed to update tournament");

            return mapper.Map<TournamentReadDto>(result);
        }
    }
}
