using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyThuVien.Models
{
    public partial class ThuVienContext : DbContext
    {
        public ThuVienContext()
        {
        }

        public ThuVienContext(DbContextOptions<ThuVienContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietPhieuThue> ChiTietPhieuThue { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<PhieuThue> PhieuThue { get; set; }
        public virtual DbSet<Sach> Sach { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=library;User ID=sa;Password=admin@123;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietPhieuThue>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.PhieuThue)
                    .WithMany(p => p.ChiTietPhieuThue)
                    .HasForeignKey(d => d.PhieuThueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietPh__Phieu__4E88ABD4");

                entity.HasOne(d => d.Sach)
                    .WithMany(p => p.ChiTietPhieuThue)
                    .HasForeignKey(d => d.SachId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietPh__SachI__4F7CD00D");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.SoDienThoai).HasMaxLength(50);

                entity.Property(e => e.TenKhachHang)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PhieuThue>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NgayThue)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NgayTra).HasColumnType("datetime");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Ðang mu?n')");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuThue)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhieuThue__Khach__4BAC3F29");
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TacGia).HasMaxLength(255);

                entity.Property(e => e.TenSach)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.TheLoai)
                    .WithMany(p => p.Sach)
                    .HasForeignKey(d => d.TheLoaiId)
                    .HasConstraintName("FK__Sach__TheLoaiId__44FF419A");
            });

            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TenLoai)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
