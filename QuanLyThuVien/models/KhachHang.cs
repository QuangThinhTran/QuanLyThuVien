using System;
using System.Collections.Generic;

namespace QuanLyThuVien.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            PhieuThue = new HashSet<PhieuThue>();
        }

        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public virtual ICollection<PhieuThue> PhieuThue { get; set; }
    }
}
