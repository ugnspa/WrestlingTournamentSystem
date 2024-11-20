using WrestlingTournamentSystem.DataAccess.DTO.User;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegisterUserDTO registerUserDTO);
        public Task<SuccessfulLoginDTO> Login(LoginUserDTO loginUserDTO);
        public Task<string> CreateRefreshToken(Guid sessionId, string userId);
        public Task<SuccessfulLoginDTO> GetAccessTokenFromRefreshToken(string? refreshToken);
        public string GetSessionIdFromRefreshToken(string? refreshToken);
        public Task<IEnumerable<CoachListDTO>> GetCoachesAsync();
        public Task<CoachDetailDTO> GetCoachWithWrestlersAsync(string userId);
    }
}
