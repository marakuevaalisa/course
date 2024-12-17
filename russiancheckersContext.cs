using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WinFormsApp1
{
    public partial class russiancheckersContext : DbContext
    {
        public russiancheckersContext()
        {
        }

        public russiancheckersContext(DbContextOptions<russiancheckersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HistoryOfGame> HistoyOfGames { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=russian-checkers;Username=postgres;Password=Mar29092002");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<HistoryOfGame>(entity =>
            {
                entity.ToTable("historyOfGame");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Player)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Winner)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.IdGame)
                    .HasName("log_pkey");

                entity.ToTable("log");

                entity.Property(e => e.IdGame).HasColumnName("id_game");

                entity.Property(e => e.History)
                    .HasColumnType("character varying")
                    .HasColumnName("history");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
