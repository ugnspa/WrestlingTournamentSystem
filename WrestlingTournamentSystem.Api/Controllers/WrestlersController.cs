using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/[controller]")]
    public class WrestlersController(IWrestlerService wrestlerService) : BaseController
    {
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
                var wrestlers = await wrestlerService.GetTournamentWeightCategoryWrestlersAsync(tournamentId, weightCategoryId);
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
                var wrestler = await wrestlerService.GetTournamentWeightCategoryWrestlerAsync(tournamentId, weightCategoryId, wrestlerId);
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
        /// <param name="wrestlerCreateDto">The wrestler creation data transfer object.</param>
        /// <response code="201">A newly created wrestler within the specified tournament and weight category.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="422">If the birthday is in the future.</response>
        /// <response code="404">If the tournament or weight category is not found.</response>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int weightCategoryId, WrestlerCreateDto wrestlerCreateDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var wrestlerReadDto = await wrestlerService.CreateAndAddWrestlerToTournamentWeightCategory(isAdmin, userId, tournamentId, weightCategoryId, wrestlerCreateDto);
                return Created("", ApiResponse.CreatedResponse("Created Wrestler", wrestlerReadDto));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Removes wrestler from a tournament weight category.
        /// </summary>
        /// <param name="tournamentId">The tournament's identifier.</param>
        /// <param name="weightCategoryId">The weight category's identifier.</param>
        /// <param name="wrestlerId">The wrestler's identifier to remove.</param>
        /// <response code="204">If the wrestler is successfully removed.</response>
        /// <response code="404">If the wrestler, tournament, or weight category is not found.</response>
        [HttpDelete("{wrestlerId}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> RemoveWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                await wrestlerService.RemoveWrestlerFromTournamentWeightCategoryAsync(isAdmin, userId, tournamentId, weightCategoryId, wrestlerId);
                return Ok(ApiResponse.NoContentResponse());
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
        /// <param name="wrestlerUpdateDto">The wrestler update data transfer object.</param>
        /// <response code="200">An updated wrestler if successful.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="422">If the birthday is in the future.</response>
        /// <response code="404">If the wrestler, tournament, or weight category is not found.</response>
        [HttpPut("{wrestlerId}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> UpdateWrestler(int tournamentId, int weightCategoryId, int wrestlerId, WrestlerUpdateDto wrestlerUpdateDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var wrestlerReadDto = await wrestlerService.UpdateWrestlerAsync(isAdmin, userId, tournamentId, weightCategoryId, wrestlerId, wrestlerUpdateDto);
                return Ok(ApiResponse.OkResponse("Wrestler Updated", wrestlerReadDto));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("/api/v1/Wrestlers/{id}")]
        public async Task<IActionResult> GetWrestlerById(int id)
        {
            try
            {
                var wrestler = await wrestlerService.GetWrestlerByIdAsync(id);
                return Ok(ApiResponse.OkResponse("Wrestler by id", wrestler));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("/api/v1/Wrestlers")]
        public async Task<IActionResult> GetAllWrestlers()
        {
            try
            {
                var wrestlers = await wrestlerService.GetAllWrestlersAsync();
                return Ok(ApiResponse.OkResponse("All Wrestlers", wrestlers));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("/api/v1/Wrestlers/WrestlingStyles")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> GetWrestlingStyles()
        {
            try
            {
                var wrestlingStyles = await wrestlerService.GetWrestlingStylesAsync();
                return Ok(ApiResponse.OkResponse("Wrestling Styles", wrestlingStyles));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
