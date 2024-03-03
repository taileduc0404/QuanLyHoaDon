using System;
using HoaDon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HoaDon.Data
{
    public partial class hoadonContext : DbContext
    {
        public hoadonContext()
        {
        }

        public hoadonContext(DbContextOptions<hoadonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }
        public virtual DbSet<Hanghoa> Hanghoas { get; set; }
        public virtual DbSet<Hoadon> Hoadons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ASUS;Database=HoaDon;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Chitiethoadon>(entity =>
            {
                entity.HasKey(e => new { e.Sohd, e.Mahang });

                entity.ToTable("chitiethoadon");

                entity.Property(e => e.Sohd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sohd");

                entity.Property(e => e.Mahang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mahang");

                entity.Property(e => e.Dongia).HasColumnName("dongia");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.HasOne(d => d.MahangNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.Mahang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_chitiethoadon_hanghoa");

                entity.HasOne(d => d.SohdNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.Sohd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_chitiethoadon_hoadon");
            });

            modelBuilder.Entity<Hanghoa>(entity =>
            {
                entity.HasKey(e => e.Mahang);

                entity.ToTable("hanghoa");

                entity.Property(e => e.Mahang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mahang");

                entity.Property(e => e.Dongia).HasColumnName("dongia");

                entity.Property(e => e.Dvt)
                    .HasMaxLength(50)
                    .HasColumnName("dvt");

                entity.Property(e => e.Tenhang)
                    .HasMaxLength(50)
                    .HasColumnName("tenhang");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.Sohd);

                entity.ToTable("hoadon");

                entity.Property(e => e.Sohd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sohd");

                entity.Property(e => e.Ngaylaphd)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaylaphd");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(50)
                    .HasColumnName("tenkh");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
