using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Exceptions;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/[controller]")]
    public class WrestlersController : BaseController
    {
        private readonly IWrestlerService _wrestlerService;

        public WrestlersController(IWrestlerService wrestlerService)
        { 
            _wrestlerService = wrestlerService;
        }

        /// <summary>
        /// Gets all wrestlers within a specific tournament and weight category.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <response code="200">Returns a list of wrestlers in the specified tournament and weight category.</response>
        /// <response code="404">If the tournament or weight category is not found.</response>
        [HttpGet]
        public async Task<IActionResult> GetTournamentWeightCategoryWrestlers(int tournamentId, int weightCategoryId)
        {
            try
            {
                return Ok(await _wrestlerService.GetTournamentWeightCategoryWrestlersAsync(tournamentId, weightCategoryId));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Retrieves a specific wrestler within a tournament and weight category.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <param name="wrestlerId">The wrestler's identifier.</param>
        /// <response code="200">Details of the requested wrestler.</response>
        /// <response code="404">If the wrestler, tournament, or weight category is not found.</response>
        [HttpGet("{wrestlerId}")]
        public async Task<IActionResult> GetTournamentWeightCategoryWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                return Ok(await _wrestlerService.GetTournamentWeightCategoryWrestlerAsync(tournamentId, weightCategoryId, wrestlerId));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Adds a new wrestler to a tournament weight category.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <param name="wrestlerCreateDTO">The wrestler creation data transfer object.</param>
        /// <response code="201">A newly created wrestler within the specified tournament and weight category.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="422">If the birthday is in the future.</response>
        /// <response code="404">If the tournament or weight category is not found.</response>
        [HttpPost]
        public async Task<IActionResult> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int weightCategoryId, WrestlerCreateDTO wrestlerCreateDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var wrestlerReadDTO = await _wrestlerService.CreateAndAddWrestlerToTournamentWeightCategory(tournamentId, weightCategoryId, wrestlerCreateDTO);
                return Created("", wrestlerReadDTO);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Deletes a wrestler from a tournament weight category.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <param name="wrestlerId">The wrestler's identifier to delete.</param>
        /// <response code="204">If the wrestler is successfully deleted.</response>
        /// <response code="404">If the wrestler, tournament, or weight category is not found.</response>
        [HttpDelete("{wrestlerId}")]
        public async Task<IActionResult> DeleteWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                await _wrestlerService.DeleteWrestlerAsync(tournamentId, weightCategoryId, wrestlerId);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Updates the details of an existing wrestler in a tournament weight category.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <param name="wrestlerId">The wrestler's identifier to update.</param>
        /// <param name="wrestlerUpdateDTO">The wrestler update data transfer object.</param>
        /// <response code="200">An updated wrestler if successful.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="422">If the birthday is in the future.</response>
        /// <response code="404">If the wrestler, tournament, or weight category is not found.</response>
        [HttpPut("{wrestlerId}")]
        public async Task<IActionResult> UpdateWrestler(int tournamentId, int weightCategoryId, int wrestlerId, WrestlerUpdateDTO wrestlerUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var wrestlerReadDTO = await _wrestlerService.UpdateWrestlerAsync(tournamentId, weightCategoryId, wrestlerId, wrestlerUpdateDTO);
                return Ok(wrestlerReadDTO);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
