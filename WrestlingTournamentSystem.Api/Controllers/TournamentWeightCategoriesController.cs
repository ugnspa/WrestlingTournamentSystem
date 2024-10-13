using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Exceptions;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Tournaments/{tournamentId}/[controller]")]
    public class TournamentWeightCategoriesController : BaseController
    {
        private readonly ITournamentWeightCategoryService _tournamentWeightCategoryService;

        public TournamentWeightCategoriesController(ITournamentWeightCategoryService tournamentWeightCategoryService)
        {
            _tournamentWeightCategoryService = tournamentWeightCategoryService;
        }

        /// <summary>
        /// Retrieves all weight categories for a given tournament.
        /// </summary>
        /// <param name="tournamentId">The ID of the tournament to retrieve weight categories for.</param>
        /// <response code="200">Returns a list of weight categories for the tournament.</response>
        /// <response code="404">If the tournament is not found.</response>
        [HttpGet]
        public async Task<IActionResult> GetTournamentWeightCategories(int tournamentId)
        {
            try
            {
                return Ok(await _tournamentWeightCategoryService.GetTournamentWeightCategoriesAsync(tournamentId));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Retrieves a specific weight category by ID within a tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament ID.</param>
        /// <param name="weightCategoryId">The weight category ID to retrieve.</param>
        /// <response code="200">The weight category details if found.</response>
        /// <response code="404">If the weight category or tournament is not found.</response>
        [HttpGet("{weightCategoryId}")]
        public async Task<IActionResult> GetTournamentWeightCategory(int tournamentId, int weightCategoryId)
        {
            try
            {
                return Ok(await _tournamentWeightCategoryService.GetTournamentWeightCategoryAsync(tournamentId, weightCategoryId));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Deletes a specific weight category from a tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament ID.</param>
        /// <param name="weightCategoryId">The weight category ID to delete.</param>
        /// <response code="204">If the weight category is successfully deleted.</response>
        /// <response code="404">If the weight category or tournament is not found.</response>
        [HttpDelete("{weightCategoryId}")]
        public async Task<IActionResult> DeleteTournamentWeightCategory(int tournamentId, int weightCategoryId)
        {
            try
            {
                await _tournamentWeightCategoryService.DeleteTournamentWeightCategoryAsync(tournamentId, weightCategoryId);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        /// <summary>
        /// Creates a new weight category within a tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament ID.</param>
        /// <param name="tournamentWeightCategoryCreateDTO">The weight category creation details.</param>
        /// <response code="201">A newly created weight category.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="422">If the end date is less than start date or dates are out of tournament date range.</response>
        /// <response code="404">If the tournament or status is not found.</response>
        [HttpPost]
        public async Task<IActionResult> CreateTournamentWeightCategory(int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tournamentWeightCategoryRead = await _tournamentWeightCategoryService.CreateTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryCreateDTO);
                return Created("", tournamentWeightCategoryRead);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Updates a specific weight category within a tournament.
        /// </summary>
        /// <param name="tournamentId">The tournament ID.</param>
        /// <param name="weightCategoryId">The weight category ID to update.</param>
        /// <param name="tournamentWeightCategoryUpdateDTO">The new details for the weight category.</param>
        /// <response code="200">An updated weight category if details are correct.</response>
        /// <response code="400">If the details are not correct.</response>
        /// <response code="422">If the end date is less than start date or dates are out of tournament date range.</response>
        /// <response code="404">If the tournament, weight category, or status is not found.</response>
        [HttpPut("{weightCategoryId}")]
        public async Task<IActionResult> UpdateTournamentWeightCategory(int tournamentId, int weightCategoryId, TournamentWeightCategoryUpdateDTO tournamentWeightCategoryUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tournamentWeightCategoryRead = await _tournamentWeightCategoryService.UpdateTournamentWeightCategoryAsync(tournamentId, weightCategoryId, tournamentWeightCategoryUpdateDTO);
                return Ok(tournamentWeightCategoryRead);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
