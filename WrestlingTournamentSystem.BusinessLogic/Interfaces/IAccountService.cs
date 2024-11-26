using WrestlingTournamentSystem.DataAccess.DTO.User;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        public Task<UserListDto> Register(RegisterUserDto registerUserDto);
        public Task<SuccessfulLoginDto> Login(LoginUserDto loginUserDto);
        public Task<string> CreateRefreshToken(Guid sessionId, string userId);
        public Task<SuccessfulLoginDto> GetAccessTokenFromRefreshToken(string? refreshToken);
        public string GetSessionIdFromRefreshToken(string? refreshToken);
        public Task<IEnumerable<UserListDto>> GetCoachesAsync();
        public Task<UserDetailDto> GetCoachWithWrestlersAsync(string userId);
    }
}
