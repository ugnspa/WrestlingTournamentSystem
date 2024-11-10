using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Interfaces
{
    public interface ISessionRepository
    {
        Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);
        Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt);
        Task<Session?> GetSessionByIdAsync(Guid sessionId);
        Task InvalidateSessionAsycn(Guid sessionId);
    }
}
