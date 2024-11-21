using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;
using WrestlingTournamentSystem.DataAccess.Response;

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
                var wrestlers = await _wrestlerService.GetTournamentWeightCategoryWrestlersAsync(tournamentId, weightCategoryId);
                return Ok(ApiResponse.OkResponse("Wrestlers", wrestlers));
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
                var wrestler = await _wrestlerService.GetTournamentWeightCategoryWrestlerAsync(tournamentId, weightCategoryId, wrestlerId);
                return Ok(ApiResponse.OkResponse("Wrestler by id", wrestler));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Adds a new wrestler to a tournament weight category without coach details.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <param name="wrestlerCreateDTO">The wrestler creation data transfer object.</param>
        /// <response code="201">A newly created wrestler within the specified tournament and weight category.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="422">If the birthday is in the future.</response>
        /// <response code="404">If the tournament or weight category is not found.</response>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int weightCategoryId, WrestlerCreateDTO wrestlerCreateDTO)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var wrestlerReadDTO = await _wrestlerService.CreateAndAddWrestlerToTournamentWeightCategory(isAdmin, userId, tournamentId, weightCategoryId, wrestlerCreateDTO);
                return Created("", ApiResponse.CreatedResponse("Created Wrestler", wrestlerReadDTO));
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
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> DeleteWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                await _wrestlerService.DeleteWrestlerAsync(isAdmin, userId, tournamentId, weightCategoryId, wrestlerId);
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
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> UpdateWrestler(int tournamentId, int weightCategoryId, int wrestlerId, WrestlerUpdateDTO wrestlerUpdateDTO)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var wrestlerReadDTO = await _wrestlerService.UpdateWrestlerAsync(isAdmin, userId, tournamentId, weightCategoryId, wrestlerId, wrestlerUpdateDTO);
                return Ok(ApiResponse.OkResponse("Wrestler Updated", wrestlerReadDTO));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
