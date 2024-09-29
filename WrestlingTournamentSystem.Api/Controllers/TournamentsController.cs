using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Tournament;
using WrestlingTournamentSystem.DataAccess.Exceptions;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TournamentsController : BaseController
    {
        private readonly ITournamentsService _tournamentsService;
        public TournamentsController(ITournamentsService tournamentsService)
        {
            _tournamentsService = tournamentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTournaments()
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
            catch (Exception e)
            {
                return HandleException(e);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament(TournamentCreateDTO tournamentCreateDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);  
            
            try
            {
                var tournamentReadDTO = await _tournamentsService.CreateTournamentAsync(tournamentCreateDTO);
                return Created("", tournamentReadDTO);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("{tournamentId}")]
        public async Task<IActionResult> UpdateTournament(int tournamentId, TournamentUpdateDTO tournamentUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tournamentReadDTO = await _tournamentsService.UpdateTournamentAsync(tournamentId, tournamentUpdateDTO);
                return Ok(tournamentReadDTO);
            }
            catch (Exception e)
            {
                return HandleException(e);
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
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }

}
