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
            catch (Exception e)
            {
                return HandleException(e);
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
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
