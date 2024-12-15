using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.Helpers.Responses;
using WrestlingTournamentSystem.DataAccess.Helpers.Roles;
using WrestlingTournamentSystem.DataAccess.Helpers.Settings;

namespace WrestlingTournamentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountsController(
        IAccountService accountsService,
        ISessionService sessionService,
        IOptions<JwtSettings> jwtSettings)
        : BaseController
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            try
            {
                var user = await accountsService.Register(registerUserDto);
                return Created("", ApiResponse.CreatedResponse("User registered", user));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            try
            {
                var loginDto = await accountsService.Login(loginUserDto);

                var sessionId = Guid.NewGuid();

                var refreshToken = await accountsService.CreateRefreshToken(sessionId, loginDto.UserId);

                await sessionService.CreateSessionAsync(sessionId, loginDto.UserId, refreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

                UpdateCookie(refreshToken);

                return Ok(ApiResponse.OkResponse("Successful login", new AccessTokenDto(loginDto.AccessToken)));
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

            if (string.IsNullOrEmpty(refreshToken))
                return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse("Refresh token not found"));

            try
            {
                var loginDto = await accountsService.GetAccessTokenFromRefreshToken(refreshToken);

                var sessionId = Guid.Parse(accountsService.GetSessionIdFromRefreshToken(refreshToken));

                if(!await sessionService.IsSessionValidAsync(sessionId, refreshToken)) 
                {
                    return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse("Session is not valid anymore"));
                }

                var newRefreshToken = await accountsService.CreateRefreshToken(sessionId, loginDto.UserId);

                UpdateCookie(newRefreshToken);

                await sessionService.ExtendSessionAsync(sessionId, newRefreshToken, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays));

                return Ok (ApiResponse.OkResponse("Access token refreshed", new AccessTokenDto(loginDto.AccessToken)));
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
                return UnprocessableEntity(ApiResponse.UnprocessableEntityResponse("Refresh token not found"));

            try
            {
                var sessionId = Guid.Parse(accountsService.GetSessionIdFromRefreshToken(refreshToken));

                await sessionService.InvalidateSessionAsync(sessionId);

                DeleteCookie("RefreshToken");

                return Ok(ApiResponse.OkResponse("Logout successful"));
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
                var coaches = await accountsService.GetCoachesAsync();
                return Ok(ApiResponse.OkResponse("Coaches", coaches));
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
                var coach = await accountsService.GetCoachWithWrestlersAsync(id);
                return Ok(ApiResponse.OkResponse("Coach with wrestlers", coach));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAdminWithWrestlers()
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (String.IsNullOrEmpty(userId))
                    return Unauthorized(ApiResponse.UnauthorizedResponse("User ID is missing from the token."));

                var admin = await accountsService.GetAdminWtihWrestlersAsync(userId);
                return Ok(ApiResponse.OkResponse("Admin with Wrestlers", admin));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("Roles")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await accountsService.GetAllRolesAsync();
                return Ok(ApiResponse.OkResponse("Roles", roles));
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
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
                Secure = true
            };

            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }

        private void DeleteCookie(string cookieName)
        {
            HttpContext.Response.Cookies.Append(cookieName, string.Empty, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }

    }
}
