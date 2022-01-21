using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InmobiliariaNetCore.Models
{
    public partial class inmobiliariaContext : DbContext
    {
        public inmobiliariaContext()
        {
        }

        public inmobiliariaContext(DbContextOptions<inmobiliariaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CsActivity> CsActivities { get; set; } = null!;
        public virtual DbSet<CsPropierty> CsPropierties { get; set; } = null!;
        public virtual DbSet<CsSurvey> CsSurveys { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=inmobiliaria;Username=postgres;Password=admin123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("C");

            modelBuilder.Entity<CsActivity>(entity =>
            {
                entity.ToTable("cs_activity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.PropiertyId).HasColumnName("propierty_id");

                entity.Property(e => e.Schedule)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("schedule");

                entity.Property(e => e.Status)
                    .HasMaxLength(35)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Propierty)
                    .WithMany(p => p.CsActivities)
                    .HasForeignKey(d => d.PropiertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cs_activity_propierty_id_fkey");
            });

            modelBuilder.Entity<CsPropierty>(entity =>
            {
                entity.ToTable("cs_propierty");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DisabledAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("disabled_at");

                entity.Property(e => e.Status)
                    .HasMaxLength(35)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<CsSurvey>(entity =>
            {
                entity.ToTable("cs_survey");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.Answers)
                    .HasColumnType("json")
                    .HasColumnName("answers");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.CsSurveys)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("cs_survey_activity_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
