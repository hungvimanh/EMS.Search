using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EMS.Search.Repositories.Models
{
    public partial class EMSContext : DbContext
    {
        public virtual DbSet<MajorsDAO> Majors { get; set; }
        public virtual DbSet<SubjectGroupDAO> SubjectGroup { get; set; }
        public virtual DbSet<UniversityDAO> University { get; set; }
        public virtual DbSet<University_MajorsDAO> University_Majors { get; set; }
        public virtual DbSet<University_Majors_SubjectGroupDAO> University_Majors_SubjectGroup { get; set; }

        public EMSContext(DbContextOptions<EMSContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=.;initial catalog=EMS;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MajorsDAO>(entity =>
            {
                entity.ToTable("Majors", "UNV");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<SubjectGroupDAO>(entity =>
            {
                entity.ToTable("SubjectGroup", "HS");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<UniversityDAO>(entity =>
            {
                entity.ToTable("University", "UNV");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descreption).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Website).HasMaxLength(50);
            });

            modelBuilder.Entity<University_MajorsDAO>(entity =>
            {
                entity.ToTable("University_Majors", "UNV");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.HasOne(d => d.Majors)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_Majors");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.University_Majors)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_University");
            });

            modelBuilder.Entity<University_Majors_SubjectGroupDAO>(entity =>
            {
                entity.ToTable("University_Majors_SubjectGroup", "UNV");

                entity.Property(e => e.Benchmark).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.SubjectGroup)
                    .WithMany(p => p.University_Majors_SubjectGroups)
                    .HasForeignKey(d => d.SubjectGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_SubjectGroup_SubjectGroup");

                entity.HasOne(d => d.University_Majors)
                    .WithMany(p => p.University_Majors_SubjectGroups)
                    .HasForeignKey(d => d.University_MajorsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_University_Majors_SubjectGroup_University_Majors");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
