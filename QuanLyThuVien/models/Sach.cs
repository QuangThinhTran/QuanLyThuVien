using System;
using System.Collections.Generic;

namespace QuanLyThuVien.Models
{
    public partial class Sach
    {
        public Sach()
        {
            ChiTietPhieuThue = new HashSet<ChiTietPhieuThue>();
        }

        public int Id { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public int? NamXuatBan { get; set; }
        public int? TheLoaiId { get; set; }
        public int SoLuong { get; set; }

        public virtual TheLoai TheLoai { get; set; }
        public virtual ICollection<ChiTietPhieuThue> ChiTietPhieuThue { get; set; }
    }
}
