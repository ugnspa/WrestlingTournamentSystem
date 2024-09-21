using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Data
{
    public class WrestlingTournamentSystemDbContext : DbContext
    {
        public WrestlingTournamentSystemDbContext(DbContextOptions<WrestlingTournamentSystemDbContext> options) : base(options)
        {
        }

        public DbSet<TournamentStatus> TournamentStatuses { get; set; }
        public DbSet<WrestlingStyle> WrestlingStyles { get; set; }
        public DbSet<TournamentWeightCategoryStatus> TournamentWeightCategoryStatuses { get; set; }
    }
}
