﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WrestlingTournamentSystem.DataAccess.Data;
using WrestlingTournamentSystem.DataAccess.Entities;
using WrestlingTournamentSystem.DataAccess.Interfaces;

namespace WrestlingTournamentSystem.DataAccess.Repositories
{
    public class WrestlerRepository : IWrestlerRepository
    {
        private readonly WrestlingTournamentSystemDbContext _context;

        public WrestlerRepository(WrestlingTournamentSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Wrestler?> GetTournamentWeightCategoryWrestlerAsync(int tournamentId, int tournamentWeightCategoryId, int wrestlerId)
        {
            return await _context.TournamentWeightCategories
                 .Where(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId)
                 .SelectMany(twc => twc.Wrestlers)
                 .FirstOrDefaultAsync(w => w.Id == wrestlerId);
        }

        public async Task<IEnumerable<Wrestler>> GetTournamentWeightCategoryWrestlersAsync(int tournamentId, int tournamentWeightCategoryId)
        {
            return await _context.TournamentWeightCategories
                .Where(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId)
                .SelectMany(twc => twc.Wrestlers)
                .ToListAsync();
        }

        public async Task<Wrestler?> CreateAndAddWrestlerToTournamentWeightCategoryAsync(int tournamentId, int tournamentWeightCategoryId, Wrestler wrestler)
        {
            var tournamentWeightCategory = await _context.TournamentWeightCategories
                .Include(twc => twc.Wrestlers)
                .FirstOrDefaultAsync(twc => twc.Id == tournamentWeightCategoryId && twc.fk_TournamentId == tournamentId);

            if (tournamentWeightCategory == null)
            {
                return null;
            }

            tournamentWeightCategory.Wrestlers.Add(wrestler);
            await _context.SaveChangesAsync();
            return wrestler;
        }

        public async Task DeleteWrestlerAsync(Wrestler wrestler)
        {
            _context.Wrestlers.Remove(wrestler);
            await _context.SaveChangesAsync();
        }
    }
}
