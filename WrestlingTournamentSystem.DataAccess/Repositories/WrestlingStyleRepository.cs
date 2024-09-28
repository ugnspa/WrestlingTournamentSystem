using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WrestlingStyleRepository : IWrestlingStyleRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public WrestlingStyleRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public Task<WrestlingStyle?> GetWrestlingStyleByIdAsync(int wrestlingStyleId)
        {
            return _context.WrestlingStyles.FirstOrDefaultAsync(ws => ws.Id == wrestlingStyleId);
        }

        public Task<bool> WrestlingStyleExistsAsync(int wrestlingStyleId)
        {
            return _context.WrestlingStyles.AnyAsync(ws => ws.Id == wrestlingStyleId);
        }
    }
}
