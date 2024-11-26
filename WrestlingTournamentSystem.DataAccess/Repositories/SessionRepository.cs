using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class SessionRepository(WrestlingTournamentSystemDbContext context) : ISessionRepository
    {
        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            var session = new Session
            {
                Id = sessionId,
                UserId = userId,
                InitiatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                LastRefreshToken = refreshToken.ToSha256()
            };

            context.Sessions.Add(session);

            await context.SaveChangesAsync();
        }

        public async Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            var session = context.Sessions.FirstOrDefault(s => s.Id == sessionId) ?? throw new NotFoundException("Session not found");

            session.ExpiresAt = expiresAt;
            session.LastRefreshToken = refreshToken.ToSha256();

            await context.SaveChangesAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(Guid sessionId)
        {
            return await context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task InvalidateSessionAsycn(Guid sessionId)
        {
            var session = context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                return;

            session.IsRevoked = true;

            await context.SaveChangesAsync();
        }
    }
}
