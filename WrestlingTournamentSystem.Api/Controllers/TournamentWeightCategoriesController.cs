﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Tournaments/{tournamentId}/[controller]")]
    public class TournamentWeightCategoriesController(ITournamentWeightCategoryService tournamentWeightCategoryService)
        : BaseController
    {
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
                var tournamentWeightCategories = await tournamentWeightCategoryService.GetTournamentWeightCategoriesAsync(tournamentId);
                return Ok(ApiResponse.OkResponse("Tournament Weight Categories", tournamentWeightCategories));
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
                var tournamentWeightCategory = await tournamentWeightCategoryService.GetTournamentWeightCategoryAsync(tournamentId, weightCategoryId);
                return Ok(ApiResponse.OkResponse("Tournament Weight Category by id", tournamentWeightCategory));
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
        /// <response code="200">If the weight category is successfully deleted.</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="404">If the weight category or tournament is not found.</response>
        [HttpDelete("{weightCategoryId}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> DeleteTournamentWeightCategory(int tournamentId, int weightCategoryId)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                await tournamentWeightCategoryService.DeleteTournamentWeightCategoryAsync(isAdmin, userId, tournamentId, weightCategoryId);
                return Ok(ApiResponse.NoContentResponse());
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
        /// <param name="tournamentWeightCategoryCreateDto">The weight category creation details.</param>
        /// <response code="201">A newly created weight category.</response>
        /// <response code="400">If the details are incorrect.</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="422">If the end date is less than start date or dates are out of tournament date range.</response>
        /// <response code="404">If the tournament or status is not found.</response>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> CreateTournamentWeightCategory(int tournamentId, TournamentWeightCategoryCreateDto tournamentWeightCategoryCreateDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var tournamentWeightCategoryRead = await tournamentWeightCategoryService.CreateTournamentWeightCategoryAsync(isAdmin, userId, tournamentId, tournamentWeightCategoryCreateDto);
                return Created("", ApiResponse.CreatedResponse("Tournament Weight Category created", tournamentWeightCategoryRead));
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
        /// <param name="tournamentWeightCategoryUpdateDto">The new details for the weight category.</param>
        /// <response code="200">An updated weight category if details are correct.</response>
        /// <response code="400">If the details are not correct.</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        /// <response code="422">If the end date is less than start date or dates are out of tournament date range.</response>
        /// <response code="404">If the tournament, weight category, or status is not found.</response>
        [HttpPut("{weightCategoryId}")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> UpdateTournamentWeightCategory(int tournamentId, int weightCategoryId, TournamentWeightCategoryUpdateDto tournamentWeightCategoryUpdateDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var isAdmin = HttpContext.User.IsInRole(UserRoles.Admin);

                var tournamentWeightCategoryRead = await tournamentWeightCategoryService.UpdateTournamentWeightCategoryAsync(isAdmin, userId, tournamentId, weightCategoryId, tournamentWeightCategoryUpdateDto);
                return Ok(ApiResponse.OkResponse("Tournament Weight Category updated", tournamentWeightCategoryRead));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }


        /// <summary>
        /// Get all tournament weight category statuses.
        /// </summary>
        /// <response code="200">all tournament weight category statuses</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        [HttpGet("/api/v1/TournamentWeightCategories/Statuses")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> GetTournamentWeightCategoryStatuses()
        {
            try
            {
                var tournamentWeightCategoryStatuses = await tournamentWeightCategoryService.GetTournamentWeightCategoryStatusesAsync();
                return Ok(ApiResponse.OkResponse("Tournament Weight Category Statuses", tournamentWeightCategoryStatuses));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }

        /// <summary>
        /// Get all tournament weight categories
        /// </summary>
        /// <response code="200">all weight categories</response>
        /// <response code="401">Not authorized</response>
        /// <response code="403">Forbidden access</response>
        [HttpGet("/api/v1/TournamentWeightCategories/WeightCategories")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.TournamentOrganiser)]
        public async Task<IActionResult> GetWeightCategories()
        {
            try
            {
               var weightCategories = await tournamentWeightCategoryService.GetWeightCategoriesAsync();
                return Ok(ApiResponse.OkResponse("All Weight Categories", weightCategories));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

        }

    }
}
