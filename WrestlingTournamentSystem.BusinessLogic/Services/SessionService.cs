using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class SessionService(ISessionRepository sessionRepository) : ISessionService
    {
        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            await sessionRepository.CreateSessionAsync(sessionId, userId, refreshToken, expiresAt);
        }

        public async Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            await sessionRepository.ExtendSessionAsync(sessionId, refreshToken, expiresAt);
        }

        public async Task InvalidateSessionAsync(Guid sessionId)
        {
            await sessionRepository.InvalidateSessionAsycn(sessionId);
        }

        public async Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken)
        {
            var session = await sessionRepository.GetSessionByIdAsync(sessionId);

            return session is not null && session.ExpiresAt > DateTime.UtcNow && !session.IsRevoked &&
                session.LastRefreshToken == refreshToken.ToSha256();
        }
    }
}
