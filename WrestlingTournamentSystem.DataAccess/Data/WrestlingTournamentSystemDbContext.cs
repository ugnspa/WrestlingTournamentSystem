using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WrestlingTournamentSystem.DataAccess.Entities;

namespace WrestlingTournamentSystem.DataAccess.Data
{
    public class WrestlingTournamentSystemDbContext : IdentityDbContext<User>
    {
        public WrestlingTournamentSystemDbContext(DbContextOptions<WrestlingTournamentSystemDbContext> options) : base(options)
        {
        }

        public DbSet<TournamentStatus> TournamentStatuses { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentWeightCategoryStatus> TournamentWeightCategoryStatuses { get; set; }
        public DbSet<TournamentWeightCategory> TournamentWeightCategories { get; set; }
        public DbSet<WrestlingStyle> WrestlingStyles { get; set; }
        public DbSet<WeightCategory> WeightCategories { get; set; }
        public DbSet<Wrestler> Wrestlers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TournamentStatus>(entity =>
            {
                entity.HasData(
                    new TournamentStatus { Id = 1 , Name = "Closed" },
                    new TournamentStatus { Id = 2 , Name = "Registration" },
                    new TournamentStatus { Id = 3, Name = "In Progress" },
                    new TournamentStatus { Id = 4, Name = "Finished" }
                );
            });

            modelBuilder.Entity<WrestlingStyle>(entity =>
            {
                entity.HasData(
                        new WrestlingStyle { Id = 1, Name = "GR" },
                        new WrestlingStyle { Id = 2, Name = "FS" },
                        new WrestlingStyle { Id = 3, Name = "WW" },
                        new WrestlingStyle { Id = 4, Name = "BW" }
                    );

            });

            modelBuilder.Entity<TournamentWeightCategoryStatus>(entity =>
            {
                entity.HasData(
                        new TournamentWeightCategoryStatus { Id = 1, Name = "Closed" },
                        new TournamentWeightCategoryStatus { Id = 2, Name = "Registration" },
                        new TournamentWeightCategoryStatus { Id = 3, Name = "Weigh-In" },
                        new TournamentWeightCategoryStatus { Id = 4, Name = "In Progress" },
                        new TournamentWeightCategoryStatus { Id = 5, Name = "Finished" }
                    );

            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasOne(t => t.TournamentStatus).WithMany().HasForeignKey(t => t.StatusId).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(t => t.TournamentWeightCategories).WithOne(twc => twc.Tournament).HasForeignKey(twc => twc.fk_TournamentId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WeightCategory>(entity =>
            {
                entity.HasOne(wc => wc.WrestlingStyle).WithMany().HasForeignKey(wc => wc.StyleId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TournamentWeightCategory>(entity =>
            {
                entity.HasOne(twc => twc.TournamentWeightCategoryStatus).WithMany().HasForeignKey(twc => twc.StatusId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(twc => twc.WeightCategory).WithMany().HasForeignKey(twc => twc.fk_WeightCategoryId).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(twc => twc.Wrestlers)
               .WithMany(w => w.TournamentWeightCategories)
               .UsingEntity<Dictionary<string, object>>(
                 "WrestlersTournamentsWeightCategories",
                 j => j.HasOne<Wrestler>().WithMany().HasForeignKey("fk_WrestlerId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasOne<TournamentWeightCategory>().WithMany().HasForeignKey("fk_TournamentWeightCategoryId").OnDelete(DeleteBehavior.Cascade)
               );
            });

            modelBuilder.Entity<Wrestler>(entity =>
            {
                entity.HasOne(w => w.WrestlingStyle).WithMany().HasForeignKey(w => w.StyleId).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(w => w.TournamentWeightCategories).WithMany(twc => twc.Wrestlers).UsingEntity<Dictionary<string, object>>(
                    "WrestlersTournamentsWeightCategories",
                    j => j.HasOne<TournamentWeightCategory>().WithMany().HasForeignKey("fk_TournamentWeightCategoryId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Wrestler>().WithMany().HasForeignKey("fk_WrestlerId").OnDelete(DeleteBehavior.Cascade)
                );
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
