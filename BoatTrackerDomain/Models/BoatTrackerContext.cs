using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BoatTrackerDomain.Models
{
    public partial class BoatTrackerContext : DbContext
    {
        public BoatTrackerContext()
        {
        }

        public BoatTrackerContext(DbContextOptions<BoatTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boat> Boats { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Boat>(entity =>
            {
                entity.HasKey(e => e.HIN)
                    .IsClustered(false);

                entity.HasIndex(e => e.Name, "IX_Boat_Name");

                entity.Property(e => e.HIN)
                    .HasMaxLength(50)
                    .HasColumnName("HIN");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.BoatState)
                    .WithMany(p => p.Boats)
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Boats_States");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.HasIndex(e => e.Description, "IX_State_Description");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
