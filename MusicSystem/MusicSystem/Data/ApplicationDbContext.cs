using Microsoft.EntityFrameworkCore;
using MusicSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicSystem.DTOs;

namespace MusicSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {;
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongPerformer> SongsPerformers { get; set; }
        public DbSet<Writer> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Writer>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Producer>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Performer>(entity => {
                entity.HasIndex(e => e.StageName).IsUnique();
            });

            modelBuilder.Entity<Album>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<MusicSystem.DTOs.WriterDto> WriterDto { get; set; }
      
    }
}
