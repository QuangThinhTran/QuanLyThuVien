using System;
using System.Collections.Generic;

namespace QuanLyThuVien.Models
{
    public partial class PhieuThue
    {
        public PhieuThue()
        {
            ChiTietPhieuThue = new HashSet<ChiTietPhieuThue>();
        }

        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public DateTime? NgayThue { get; set; }
        public DateTime? NgayTra { get; set; }
        public string TrangThai { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<ChiTietPhieuThue> ChiTietPhieuThue { get; set; }
    }
}
