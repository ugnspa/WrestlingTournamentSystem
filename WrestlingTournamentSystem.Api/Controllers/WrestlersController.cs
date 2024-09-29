using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.Wrestler;
using WrestlingTournamentSystem.DataAccess.Exceptions;

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

        [HttpGet]
        public async Task<IActionResult> GetTournamentWeightCategoryWrestlers(int tournamentId, int weightCategoryId)
        {
            try
            {
                return Ok(await _wrestlerService.GetTournamentWeightCategoryWrestlersAsync(tournamentId, weightCategoryId));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("{wrestlerId}")]
        public async Task<IActionResult> GetTournamentWeightCategoryWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                return Ok(await _wrestlerService.GetTournamentWeightCategoryWrestlerAsync(tournamentId, weightCategoryId, wrestlerId));
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAndAddWrestlerToTournamentWeightCategory(int tournamentId, int weightCategoryId, WrestlerCreateDTO wrestlerCreateDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var wrestlerReadDTO = await _wrestlerService.CreateAndAddWrestlerToTournamentWeightCategory(tournamentId, weightCategoryId, wrestlerCreateDTO);
                return Created("", wrestlerReadDTO);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("{wrestlerId}")]
        public async Task<IActionResult> DeleteWrestler(int tournamentId, int weightCategoryId, int wrestlerId)
        {
            try
            {
                await _wrestlerService.DeleteWrestlerAsync(tournamentId, weightCategoryId, wrestlerId);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("{wrestlerId}")]
        public async Task<IActionResult> UpdateWrestler(int tournamentId, int weightCategoryId, int wrestlerId, WrestlerUpdateDTO wrestlerUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var wrestlerReadDTO = await _wrestlerService.UpdateWrestlerAsync(tournamentId, weightCategoryId, wrestlerId, wrestlerUpdateDTO);
                return Ok(wrestlerReadDTO);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
