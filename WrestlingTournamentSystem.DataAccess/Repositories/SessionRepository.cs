using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Helpers;
using WrestlingTournamentSystem.DataAccess.Helpers.Exceptions;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public SessionRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            var session = new Session
            {
                Id = sessionId,
                UserId = userId,
                InitiatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt,
                LastRefreshToken = refreshToken.ToSHA256()
            };

            _context.Sessions.Add(session);

            await _context.SaveChangesAsync();
        }

        public async Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                throw new NotFoundException("Session not found");

            session.ExpiresAt = expiresAt;
            session.LastRefreshToken = refreshToken.ToSHA256();

            await _context.SaveChangesAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(Guid sessionId)
        {
            return await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task InvalidateSessionAsycn(Guid sessionId)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                return;

            session.IsRevoked = true;

            await _context.SaveChangesAsync();
        }
    }
}
