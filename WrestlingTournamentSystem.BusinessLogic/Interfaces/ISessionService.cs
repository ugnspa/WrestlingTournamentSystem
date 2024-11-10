﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrestlingTournamentSystem.BusinessLogic.Interfaces
{
    public interface ISessionService
    {
        public Task CreateSessionAsync(Guid sessionId, string userId, string refreshToken, DateTime expiresAt);
        public Task ExtendSessionAsync(Guid sessionId, string refreshToken, DateTime expiresAt);
        public Task InvalidateSessionAsync(Guid sessionId);
        public Task<bool> IsSessionValidAsync(Guid sessionId, string refreshToken);
    }
}