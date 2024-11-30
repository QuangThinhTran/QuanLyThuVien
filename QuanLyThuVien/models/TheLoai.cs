using System;
using System.Collections.Generic;

namespace QuanLyThuVien.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            Sach = new HashSet<Sach>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<Sach> Sach { get; set; }
    }
}
