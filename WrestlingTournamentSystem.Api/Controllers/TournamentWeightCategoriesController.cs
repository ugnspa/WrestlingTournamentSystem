using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.TournamentWeightCategory;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
