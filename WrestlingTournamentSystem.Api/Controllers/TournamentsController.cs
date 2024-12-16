using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;

namespace WrestlingTournamentSystem.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class TournamentsController(ITournamentsService tournamentsService) : BaseController
    {
        /// <summary>
        /// Retrieves all tournaments.
        /// </summary>
        /// <response code="200">Returns a list of tournaments.</response>
        [HttpGet]
        public async Task<IActionResult> GetTournaments()
        {
            var tournaments = await tournamentsService.GetTournamentsAsync();
            return Ok(ApiResponse.OkResponse("Tournaments", tournaments));
        }

        /// <summary>
        /// Retrieves a specific tournament by ID.
        /// </summary>
        /// <param name="id">The ID of the tournament to retrieve.</param>
        /// <response code="200">If the tournament is found.</response>
        /// <response code="404">If the tournament is not found.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament(int id)
        {
            try
            {
                var tournament = await tournamentsService.GetTournamentAsync(id);

                return Ok(ApiResponse.OkResponse("Tournament by id", tournament));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }

        /// <summary>
        /// Creates a new tournament with the given details.
        /// </summary>
        /// <param name="tournamentCreateDto">The tournament creation details.</param>
        /// <response code="201">A newly created tournament if details are correct.</response>
        /// <response code="400">If the details are not correct.</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="422">If end date is less than start date.</response>
        /// <response code="404">If tournament status or tournament was not found.</response>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> CreateTournament(TournamentCreateDto tournamentCreateDto)
        { 
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (String.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var tournamentReadDto = await tournamentsService.CreateTournamentAsync(userId, tournamentCreateDto);
                return Created("", ApiResponse.CreatedResponse("Tournament created", tournamentReadDto));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Updates an existing tournament with the specified ID.
        /// </summary>
        /// <param name="tournamentId">The ID of the tournament to update.</param>
        /// <param name="tournamentUpdateDto">The new details to update the tournament.</param>
        /// <response code="200">An updated tournament if details are correct.</response>
        /// <response code="400">If the details are not correct.</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="422">If end date is less than start date.</response>
        /// <response code="404">If tournament status or tournament was not found.</response>
        [HttpPut("{tournamentId}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> UpdateTournament(int tournamentId, TournamentUpdateDto tournamentUpdateDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (String.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var tournamentReadDto = await tournamentsService.UpdateTournamentAsync(isAdmin, userId, tournamentId, tournamentUpdateDto);
                return Ok(ApiResponse.OkResponse("Tournament updated", tournamentReadDto));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Deletes a specific tournament by ID.
        /// </summary>
        /// <param name="id">The ID of the tournament to delete.</param>
        /// <response code="200">If the tournament is successfully deleted.</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="404">If the tournament is not found.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (String.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                await tournamentsService.DeleteTournamentAsync(isAdmin, userId, id);
                return Ok(ApiResponse.NoContentResponse());
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Get all tournament statuses.
        /// </summary>
        /// <response code="200">all tournament statuses</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        [HttpGet("Statuses")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> GetTournamentStatuses()
        {
            try
            {
                var tournamentStatuses = await tournamentsService.GetTournamentStatusesAsync();
                return Ok(ApiResponse.OkResponse("Tournament statuses", tournamentStatuses));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }

}
