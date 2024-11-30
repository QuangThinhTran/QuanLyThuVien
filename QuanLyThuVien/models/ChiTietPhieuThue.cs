using System;
using System.Collections.Generic;

namespace QuanLyThuVien.Models
{
    public partial class ChiTietPhieuThue
    {
        public int Id { get; set; }
        public int PhieuThueId { get; set; }
        public int SachId { get; set; }
        public int SoLuong { get; set; }

        public virtual PhieuThue PhieuThue { get; set; }
        public virtual Sach Sach { get; set; }
    }
}
