using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Helpers.Settings;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountsService;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(IAccountService accountsService, IOptions<JwtSettings> jwtSettings)
        {
            _accountsService = accountsService;
            _jwtSettings = jwtSettings.Value;
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

                var refreshToken = await _accountsService.CreateRefreshToken(loginDTO.UserId);

                UpdateCookie(refreshToken);

                return Ok(new AccessTokenDTO(loginDTO.AccessToken));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        [Route("AccessToken")]
        public async Task<IActionResult> AccessToken()
        {
            HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken);

            try
            {
                var loginDTO = await _accountsService.GetAccessTokenFromRefreshToken(refreshToken);

                var newRefreshToken = await _accountsService.CreateRefreshToken(loginDTO.UserId);

                UpdateCookie(newRefreshToken);

                return Ok (new AccessTokenDTO(loginDTO.AccessToken));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }


        private void UpdateCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                Secure = false // should be true in production
            };

            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }
    }
}
