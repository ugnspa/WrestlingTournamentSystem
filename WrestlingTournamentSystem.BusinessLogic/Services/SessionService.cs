using WrestlingTournamentSystem.BusinessLogic.Interfaces;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.BusinessLogic.Services
{
    public class SessionService : ISessionService
    {

        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }
        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            await _sessionRepository.CreateSessionAsync(sessionId, userId, refreshToken, expiresAt);
        }

        public async Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            await _sessionRepository.ExtendSessionAsync(sessionId, refreshToken, expiresAt);
        }

        public async Task InvalidateSessionAsync(Guid sessionId)
        {
            await _sessionRepository.InvalidateSessionAsycn(sessionId);
        }

        public async Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken)
        {
            var session = await _sessionRepository.GetSessionByIdAsync(sessionId);

            return session is not null && session.ExpiresAt > DateTime.UtcNow && !session.IsRevoked &&
                session.LastRefreshToken == refreshToken.ToSHA256();
        }
    }
}
