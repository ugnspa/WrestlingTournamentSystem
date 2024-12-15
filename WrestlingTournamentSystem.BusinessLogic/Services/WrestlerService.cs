using AutoMapper;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class WrestlerService(
        IWrestlerRepository wrestlerRepository,
        IWrestlingStyleRepository wrestlingStyleRepository,
        ITournamentRepository tournamentRepository,
        ITournamentWeightCategoryRepository tournamentWeightCategoryRepository,
        IMapper mapper,
        IValidationService validationService)
        : IWrestlerService
    {
        private async Task ValidateTournamentAndWeightCategory(int tournamentId, int tournamentWeightCategoryId, bool isAdmin = false, string userId = "", bool guestActionAllowed = false)
        {
            var tournament = await tournamentRepository.GetTournamentAsync(tournamentId);

            if (tournament == null)
                throw new NotFoundException($"Tournament with id {tournamentId} does not exist");

            if (tournament.OrganiserId != userId && !isAdmin && !guestActionAllowed)
                throw new ForbiddenException("You are not allowed to perform this action for this tournament");

            var tournamentWeightCategory = await tournamentWeightCategoryRepository.GetTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId);

            if (tournamentWeightCategory == null)
                throw new NotFoundException($"Tournament does not have weight category with id {tournamentWeightCategoryId}");
        }

        public async Task<WrestlerReadDto?> CreateAndAddWrestlerToTournamentWeightCategory(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, WrestlerCreateDto wrestlerCreateDto)
        {
            if (wrestlerCreateDto == null)
            {
                throw new ArgumentNullException(nameof(wrestlerCreateDto));
            }

            await ValidateTournamentAndWeightCategory( tournamentId, tournamentWeightCategoryId, isAdmin, userId);

            var wrestlingStyleExists = await wrestlingStyleRepository.WrestlingStyleExistsAsync(wrestlerCreateDto.StyleId);

            if (!wrestlingStyleExists)
            {
                throw new NotFoundException($"Wrestling style with id {wrestlerCreateDto.StyleId} does not exist");
            }

            validationService.ValidateBirthDate(wrestlerCreateDto.BirthDate);

            var wrestler = mapper.Map<Wrestler>(wrestlerCreateDto);
            //wrestler.PhotoUrl = AzureBlobService.DefaultWrestlerPhotoUrl; //Add default photo 

            var result = await wrestlerRepository.CreateAndAddWrestlerToTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId, wrestler);

            if (result == null)
            {
                throw new Exception($"Failed to add wrestler to tournament weight category");
            }

            return mapper.Map<WrestlerReadDto>(result);
        }

        public async Task<WrestlerReadDto?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId, guestActionAllowed: true);

            var tournamentWeightCategoryWrestler = await wrestlerRepository.GetTournamentWeightCategoryWrestlerAsync(tournamentId, tournamentWeightCategoryId, wrestlerId);

            if (tournamentWeightCategoryWrestler == null)
            {
                throw new NotFoundException($"Tournament weight category does not have wrestler with id {wrestlerId}");
            }

            return mapper.Map<WrestlerReadDto>(tournamentWeightCategoryWrestler);
        }

        public async Task<IEnumerable<WrestlerReadDto>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId, guestActionAllowed: true);

            return mapper.Map<IEnumerable<WrestlerReadDto>>(await wrestlerRepository.GetTournamentWeightCategoryWrestlersAsync(tournamentId, tournamentWeightCategoryId));
        }

        public async Task RemoveWrestlerFromTournamentWeightCategoryAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId, isAdmin, userId);

            var wrestler = await wrestlerRepository.GetTournamentWeightCategoryWrestlerAsync(tournamentId, tournamentWeightCategoryId, wrestlerId);
            
            if (wrestler == null)
            {
                throw new NotFoundException($"Tournament weight category does not have wrestler with id {wrestlerId}");
            }

            await wrestlerRepository.RemoveWrestlerFromTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryId, wrestler); 
        }

        public async Task<WrestlerReadDto?> UpdateWrestlerAsync(bool isAdmin, string userId, int tournamentId, int tournamentWeightCategoryId, int wrestlerId, WrestlerUpdateDto wrestlerUpdateDto)
        {
            await ValidateTournamentAndWeightCategory(tournamentId, tournamentWeightCategoryId, isAdmin, userId);

            var tounamentWeightCategoryWrestler = await wrestlerRepository.GetTournamentWeightCategoryWrestlerAsync(tournamentId, tournamentWeightCategoryId, wrestlerId);

            if (tounamentWeightCategoryWrestler == null)
                throw new NotFoundException($"Tournament weight category does not have wrestler with id {wrestlerId}");
            
            var wrestlingStyleExists = await wrestlingStyleRepository.WrestlingStyleExistsAsync(wrestlerUpdateDto.StyleId);

            if (!wrestlingStyleExists)
                throw new NotFoundException($"Wrestling style with id {wrestlerUpdateDto.StyleId} does not exist");

            validationService.ValidateBirthDate(wrestlerUpdateDto.BirthDate);

            mapper.Map(wrestlerUpdateDto, tounamentWeightCategoryWrestler);

            var result = await wrestlerRepository.UpdateWrestlerAsync(tounamentWeightCategoryWrestler);

            if (result == null)
                throw new Exception($"Failed to update wrestler with id {wrestlerId}");

            return mapper.Map<WrestlerReadDto>(result);
        }

        public async Task<WrestlerReadDto> GetWrestlerByIdAsync(int wrestlerId)
        {
            var wrestler = await wrestlerRepository.GetWrestlerByIdAsync(wrestlerId);

            if (wrestler == null)
                throw new NotFoundException($"Wrestler with id {wrestlerId} was not found");

            return mapper.Map<WrestlerReadDto>(wrestler);
        }

        public async Task<IEnumerable<WrestlerReadDto>> GetAllWrestlersAsync()
        {
            var wrestlers = await wrestlerRepository.GetAllWrestlersAsync();

            return mapper.Map<IEnumerable<WrestlerReadDto>>(wrestlers);
        }
    }
}
