using Microsoft.AspNetCore.Mvc;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.User;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountsService;

        public AccountsController(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _accountsService.Register(registerUserDTO);
                return Created();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var loginDTO = await _accountsService.Login(loginUserDTO);
                return Ok(loginDTO);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
