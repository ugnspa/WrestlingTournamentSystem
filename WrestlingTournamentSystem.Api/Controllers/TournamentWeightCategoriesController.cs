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
    public class TournamentWeightCategoriesController : ControllerBase
    {
        private readonly ITournamentWeightCategoryService _tournamentWeightCategoryService;

        public TournamentWeightCategoriesController(ITournamentWeightCategoryService tournamentWeightCategoryService)
        {
            _tournamentWeightCategoryService = tournamentWeightCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentWeightCategoryReadDTO>>> GetTournamentWeightCategories(int tournamentId)
        {
            try
            {
                return Ok(await _tournamentWeightCategoryService.GetTournamentWeightCategoriesAsync(tournamentId));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}");
            }
        }
        [HttpGet("{weightCategoryId}")]
        public async Task<IActionResult> GetTournamentWeightCategory(int tournamentId, int weightCategoryId)
        {
            try
            {
                return Ok(await _tournamentWeightCategoryService.GetTournamentWeightCategoryAsync(tournamentId, weightCategoryId));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}" );
            }
        }

        [HttpDelete("{weightCategoryId}")]
        public async Task<IActionResult> DeleteTournamentWeightCategory(int tournamentId, int weightCategoryId)
        {
            try
            {
                await _tournamentWeightCategoryService.DeleteTournamentWeightCategoryAsync(tournamentId, weightCategoryId);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournamentWeightCategory(int tournamentId, TournamentWeightCategoryCreateDTO tournamentWeightCategoryCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tournamentWeightCategoryCreateDTO.StartDate > tournamentWeightCategoryCreateDTO.EndDate)
                return UnprocessableEntity("Start date must be before end date");

            try
            {
                var tournamentWeightCategoryRead = await _tournamentWeightCategoryService.CreateTournamentWeightCategoryAsync(tournamentId, tournamentWeightCategoryCreateDTO);
                return Created("", tournamentWeightCategoryRead);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}");
            }
        }

        [HttpPut("{weightCategoryId}")]
        public async Task<IActionResult> UpdateTournamentWeightCategory(int tournamentId, int weightCategoryId, TournamentWeightCategoryUpdateDTO tournamentWeightCategoryUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (tournamentWeightCategoryUpdateDTO.StartDate > tournamentWeightCategoryUpdateDTO.EndDate)
                return UnprocessableEntity("Start date must be before end date");

            try
            {
                var tournamentWeightCategoryRead = await _tournamentWeightCategoryService.UpdateTournamentWeightCategoryAsync(tournamentId, weightCategoryId, tournamentWeightCategoryUpdateDTO);
                return Ok(tournamentWeightCategoryRead);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {e.Message}");
            }
        }
    }
}
