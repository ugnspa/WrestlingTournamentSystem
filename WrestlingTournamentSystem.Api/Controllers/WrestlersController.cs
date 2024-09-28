﻿using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Exceptions;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Tournaments/{tournamentId}/TournamentWeightCategories/{weightCategoryId}/[controller]")]
    public class WrestlersController : ControllerBase
    {
        private readonly IWrestlerService _wrestlerService;

        public WrestlersController(IWrestlerService wrestlerService)
        { 
            _wrestlerService = wrestlerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WrestlerReadDTO>>> GetTournamentWeightCategoryWrestlers(int tournamentId, int weightCategoryId)
        {
            try
            {
                return Ok(await _wrestlerService.GetTournamentWeightCategoryWrestlersAsync(tournamentId, weightCategoryId));
            ;}
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}");
            }
        }

        [HttpGet("{wrestlerId}")]
        public async Task<IActionResult> GetTournamentWeightCategoryWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                return Ok(await _wrestlerService.GetTournamentWeightCategoryWrestlerAsync(tournamentId, weightCategoryId, wrestlerId));
            }
            catch(NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}");
            }
        }
    }
}