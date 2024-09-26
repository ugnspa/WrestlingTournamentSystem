using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentsService _tournamentsService;
        public TournamentsController(ITournamentsService tournamentsService)
        {
            _tournamentsService = tournamentsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentReadDTO>>> GetTournaments()
        {
            return Ok(await _tournamentsService.GetTournamentsAsync());
        }

        [HttpGet("{id}")]   
        public async Task<IActionResult> GetTournament(int id)
        {
            try
            {
                var tournament = await _tournamentsService.GetTournamentAsync(id);

                return Ok(tournament);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament(TournamentCreateDTO tournamentCreateDTO)
        {
            if (tournamentCreateDTO.StartDate > tournamentCreateDTO.EndDate)
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "Start date must be before end date");
            
            try
            {
                var tournamentReadDTO = await _tournamentsService.CreateTournamentAsync(tournamentCreateDTO);
                return Created("", tournamentReadDTO);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("{tournamentId}")]
        public async Task<IActionResult> UpdateTournament(int tournamentId, TournamentUpdateDTO tournamentUpdateDTO)
        {
            if (tournamentUpdateDTO.StartDate > tournamentUpdateDTO.EndDate)
                return StatusCode(StatusCodes.Status422UnprocessableEntity, "Start date must be before end date");

            try
            {
                var tournamentReadDTO = await _tournamentsService.UpdateTournamentAsync(tournamentId, tournamentUpdateDTO);
                return Ok(tournamentReadDTO);
            }
            catch(ArgumentException e) 
            {                
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            try
            {
                await _tournamentsService.DeleteTournamentAsync(id);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }

}
