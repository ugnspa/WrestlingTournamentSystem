using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.BusinessLogic.Validation;
using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class AccountService(
        JwtTokenService jwtTokenService,
        IValidationService validationService,
        IMapper mapper,
        IAccountRepository accountRepository)
        : IAccountService
    {
        public async Task<UserListDto> Register(RegisterUserDto registerUserDto)
        {
            var user = await accountRepository.FindByUsernameAsync(registerUserDto.UserName);

            if (user != null)
                throw new BusinessRuleValidationException($"Username {registerUserDto.UserName} already taken");

            validationService.ValidateRegisterPassword(registerUserDto.Password, registerUserDto.ConfirmPassword);

            var newUser = mapper.Map<User>(registerUserDto);

            await accountRepository.CreateUserAsync(newUser, registerUserDto.Password);

            return mapper.Map<UserListDto>(newUser);
        }

        public async Task<SuccessfulLoginDto> Login(LoginUserDto loginUserDto)
        {
            var user = await accountRepository.FindByUsernameAsync(loginUserDto.UserName);

            if (user == null)
                throw new BusinessRuleValidationException("Invalid username or password");

            var isPasswordValid = await accountRepository.IsPasswordValidAsync(user, loginUserDto.Password);

            if (!isPasswordValid)
                throw new BusinessRuleValidationException("Invalid username or password");

            var userRoles = await accountRepository.GetUserRolesAsync(user);

            var accessToken = jwtTokenService.CreateAccessToken(user.UserName!, user.Id, userRoles);

            return new SuccessfulLoginDto(user.Id, accessToken);
        }

        public async Task<string> CreateRefreshToken(Guid sessionId, string userId)
        {
            var user = await accountRepository.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User was not found");

            return jwtTokenService.CreateRefreshToken(sessionId, user.Id);
        }

        public async Task<SuccessfulLoginDto> GetAccessTokenFromRefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken) || 
                !jwtTokenService.TryParseRefreshToken(refreshToken, out var claims) ||
                claims?.FindFirstValue(JwtRegisteredClaimNames.Sub) is not string userId)
            {
                throw new BusinessRuleValidationException("Invalid refresh token");
            }

            var user = await accountRepository.FindByIdAsync(userId);

            if(user == null)  
                throw new BusinessRuleValidationException("Invalid refresh token");

            var userRoles = await accountRepository.GetUserRolesAsync(user);

            var accessToken = jwtTokenService.CreateAccessToken(user.UserName!, user.Id, userRoles);

            return new SuccessfulLoginDto(user.Id, accessToken);
        }

        public string GetSessionIdFromRefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken) ||
                !jwtTokenService.TryParseRefreshToken(refreshToken, out var claims) ||
                claims?.FindFirstValue("SessionId") is not string sessionId)
            {
                throw new BusinessRuleValidationException("Invalid refresh token");
            }

            return sessionId;
        }

        public async Task<IEnumerable<UserListDto>> GetCoachesAsync()
        {
            var coaches = await accountRepository.GetCoaches();

            return mapper.Map<IEnumerable<UserListDto>>(coaches);
        }

        public async Task<UserDetailDto> GetCoachWithWrestlersAsync(string userId)
        {
            var coach = await accountRepository.GetCoachWithWrestlersAsync(userId);

            if(coach == null)
                throw new NotFoundException("Coach was not found");

            return mapper.Map<UserDetailDto>(coach);
        }
    }
}
