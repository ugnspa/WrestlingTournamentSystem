using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;
using WrestlingTournamentSystem.DataAccess.Helpers.Settings;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountsService;
        private readonly ISessionService _sessionService;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(IAccountService accountsService, ISessionService sessionService, IOptions<JwtSettings> jwtSettings)
        {
            _accountsService = accountsService;
            _sessionService = sessionService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = UserRoles.Admin)]
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

                var sessionId = Guid.NewGuid();

                var refreshToken = await _accountsService.CreateRefreshToken(sessionId, loginDTO.UserId);

                await _sessionService.CreateSessionAsync(sessionId, loginDTO.UserId, refreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

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
        [Authorize]
        public async Task<IActionResult> AccessToken()
        {
            HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
                return UnprocessableEntity(new ErrorResponse(StatusCodes.Status422UnprocessableEntity, "Refresh token not found"));

            try
            {
                var loginDTO = await _accountsService.GetAccessTokenFromRefreshToken(refreshToken);

                var sessionId = Guid.Parse(_accountsService.GetSessionIdFromRefreshToken(refreshToken));

                if(!await _sessionService.IsSessionValidAsync(sessionId, refreshToken!)) 
                {
                    return UnprocessableEntity(new ErrorResponse(StatusCodes.Status422UnprocessableEntity, "Session is not valid anymore"));
                }

                var newRefreshToken = await _accountsService.CreateRefreshToken(sessionId, loginDTO.UserId);

                UpdateCookie(newRefreshToken);

                await _sessionService.ExtendSessionAsync(sessionId, newRefreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

                return Ok (new AccessTokenDTO(loginDTO.AccessToken));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
                return UnprocessableEntity(new ErrorResponse(StatusCodes.Status422UnprocessableEntity, "Refresh token not found"));

            try
            {
                var sessionId = Guid.Parse(_accountsService.GetSessionIdFromRefreshToken(refreshToken));

                await _sessionService.InvalidateSessionAsync(sessionId);

                DeleteCookie("RefreshToken");

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("Coaches")]
        public async Task<IActionResult> GetCoaches() 
        {
            try
            {
                return Ok(await _accountsService.GetCoachesAsync());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("Coaches/{id}")]
        public async Task<IActionResult> GetCoachWithWrestlers(string id)
        {
            try
            {
                return Ok(await _accountsService.GetCoachWithWrestlersAsync(id));
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

        private void DeleteCookie(string cookieName)
        {
            HttpContext.Response.Cookies.Delete(cookieName);
        }
    }
}
