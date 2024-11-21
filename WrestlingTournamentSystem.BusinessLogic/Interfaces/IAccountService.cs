using WrestlingTournamentSystem.DataAccess.DTO.User;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        public Task<UserListDTO> Register(RegisterUserDTO registerUserDTO);
        public Task<SuccessfulLoginDTO> Login(LoginUserDTO loginUserDTO);
        public Task<string> CreateRefreshToken(Guid sessionId, string userId);
        public Task<SuccessfulLoginDTO> GetAccessTokenFromRefreshToken(string? refreshToken);
        public string GetSessionIdFromRefreshToken(string? refreshToken);
        public Task<IEnumerable<UserListDTO>> GetCoachesAsync();
        public Task<UserDetailDTO> GetCoachWithWrestlersAsync(string userId);
    }
}
