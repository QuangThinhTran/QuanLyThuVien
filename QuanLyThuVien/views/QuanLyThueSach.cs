using System;
using System.Linq;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Views
{
    public class QuanLyThueSach
    {
        public void QuanLyThueSachMenu()
        {
            var context = new ThuVienContext();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nQuản lý thuê sách:");
                Console.WriteLine("1. Thêm phiếu thuê");
                Console.WriteLine("2. Cập nhật phiếu thuê");
                Console.WriteLine("3. Hiển thị sách quá hạn");
                Console.WriteLine("4. Hiển thị thể loại sách được thuê nhiều nhất");
                Console.WriteLine("5. Hiển thị sách được thuê nhiều nhất");
                Console.WriteLine("6. Hiển thị danh sách khách hàng đã thuê sách");
                Console.WriteLine("7. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1":
                        ThemPhieuThue(context);
                        break;

                    case "2":
                        CapNhatPhieuThue(context);
                        break;

                    case "3":
                        HienThiSachQuaHan(context);
                        break;

                    case "4":
                        HienThiTheLoaiSachThueNhieuNhat(context);
                        break;

                    case "5":
                        HienThiSachThueNhieuNhat(context);
                        break;

                    case "6":
                        HienThiDanhSachKhachHangThueSach(context);
                        break;

                    case "7":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        private void ThemPhieuThue(ThuVienContext context)
        {
            Console.Write("Nhập email khách hàng: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email khách hàng là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var khachHang = context.KhachHang.FirstOrDefault(k => k.Email == email);
            if (khachHang == null)
            {
                Console.WriteLine("Không tìm thấy khách hàng với email đã nhập.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập tên sách: ");
            string tenSach = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tenSach))
            {
                Console.WriteLine("Tên sách là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var sach = context.Sach.FirstOrDefault(s => s.TenSach == tenSach);
            if (sach == null)
            {
                Console.WriteLine("Không tìm thấy sách với tên đã nhập.");
                Console.ReadKey();
                return;
            }

            if (sach.SoLuong == 0)
            {
                Console.WriteLine("Đã hết sách.");
                Console.ReadKey();
                return;
            }

            var phieuThue = new PhieuThue
            {
                KhachHangId = khachHang.Id,
                NgayThue = DateTime.Now,
                TrangThai = "Đang mượn"
            };
            context.PhieuThue.Add(phieuThue);
            context.SaveChanges();

            var chiTietPhieuThue = new ChiTietPhieuThue
            {
                PhieuThueId = phieuThue.Id,
                SachId = sach.Id,
                SoLuong = 1
            };
            context.ChiTietPhieuThue.Add(chiTietPhieuThue);
            context.SaveChanges();

            sach.SoLuong -= 1;
            context.SaveChanges();

            Console.WriteLine("Thêm phiếu thuê thành công!");
            Console.ReadKey();
        }

        private void CapNhatPhieuThue(ThuVienContext context)
        {
            Console.Write("Nhập email khách hàng: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email khách hàng là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var khachHang = context.KhachHang.FirstOrDefault(k => k.Email == email);
            if (khachHang == null)
            {
                Console.WriteLine("Không tìm thấy khách hàng với email đã nhập.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập tên sách: ");
            string tenSach = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tenSach))
            {
                Console.WriteLine("Tên sách là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var sach = context.Sach.FirstOrDefault(s => s.TenSach == tenSach);
            if (sach == null)
            {
                Console.WriteLine("Không tìm thấy sách với tên đã nhập.");
                Console.ReadKey();
                return;
            }

            var phieuThue = context.PhieuThue.FirstOrDefault(pt => pt.KhachHangId == khachHang.Id && pt.ChiTietPhieuThue.Any(ct => ct.SachId == sach.Id && pt.TrangThai == "Đang mượn"));
            if (phieuThue == null)
            {
                Console.WriteLine("Không tìm thấy phiếu thuê với thông tin đã nhập.");
                Console.ReadKey();
                return;
            }

            phieuThue.NgayTra = DateTime.Now;
            phieuThue.TrangThai = "Đã trả";
            context.SaveChanges();

            sach.SoLuong += 1;
            context.SaveChanges();

            Console.WriteLine("Cập nhật phiếu thuê thành công!");
            Console.ReadKey();
        }

        private void HienThiSachQuaHan(ThuVienContext context)
        {
            var ngayHienTai = DateTime.Now;
            var sachQuaHan = context.PhieuThue
                .Where(pt => pt.TrangThai == "Đang mượn" && pt.NgayThue.HasValue && pt.NgayThue.Value.AddDays(30) < ngayHienTai)
                .SelectMany(pt => pt.ChiTietPhieuThue)
                .Select(ct => new
                {
                    ct.Sach.TenSach,
                    ct.Sach.TacGia,
                    ct.Sach.NamXuatBan,
                    KhachHang = ct.PhieuThue.KhachHang.TenKhachHang,
                    ct.PhieuThue.KhachHang.Email
                })
                .ToList();

            Console.WriteLine("Danh sách sách quá hạn:");
            foreach (var item in sachQuaHan)
            {
                Console.WriteLine($"Tên sách: {item.TenSach}, Tác giả: {item.TacGia}, Năm xuất bản: {item.NamXuatBan}, Khách hàng: {item.KhachHang}, Email: {item.Email}");
            }
            Console.ReadKey();
        }

        private void HienThiTheLoaiSachThueNhieuNhat(ThuVienContext context)
        {
            var theLoaiThueNhieuNhat = context.ChiTietPhieuThue
                .GroupBy(ct => ct.Sach.TheLoai.TenLoai)
                .Select(g => new
                {
                    TheLoai = g.Key,
                    SoLuong = g.Sum(ct => ct.SoLuong)
                })
                .OrderByDescending(g => g.SoLuong)
                .ToList();

            Console.WriteLine("Thể loại sách được thuê nhiều nhất:");
            foreach (var item in theLoaiThueNhieuNhat)
            {
                Console.WriteLine($"Thể loại: {item.TheLoai}, Số lượng: {item.SoLuong}");
            }
            Console.ReadKey();
        }

        private void HienThiSachThueNhieuNhat(ThuVienContext context)
        {
            var sachThueNhieuNhat = context.ChiTietPhieuThue
                .GroupBy(ct => ct.Sach.TenSach)
                .Select(g => new
                {
                    TenSach = g.Key,
                    SoLuong = g.Sum(ct => ct.SoLuong)
                })
                .OrderByDescending(g => g.SoLuong)
                .ToList();

            Console.WriteLine("Sách được thuê nhiều nhất:");
            foreach (var item in sachThueNhieuNhat)
            {
                Console.WriteLine($"Tên sách: {item.TenSach}, Số lượng: {item.SoLuong}");
            }
            Console.ReadKey();
        }

        private void HienThiDanhSachKhachHangThueSach(ThuVienContext context)
        {
            var khachHangThueSach = context.PhieuThue
                .Select(pt => pt.KhachHang)
                .Distinct()
                .ToList();

            Console.WriteLine("Danh sách khách hàng đã thuê sách:");
            foreach (var khachHang in khachHangThueSach)
            {
                Console.WriteLine($"Tên: {khachHang.TenKhachHang}, Email: {khachHang.Email}");
            }
            Console.ReadKey();
        }
    }

    class QuanLyLoaiSach
    {
        public void QuanLyLoaiSachMenu()
        {
            var context = new ThuVienContext();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nQuản lý thể loại sách:");
                Console.WriteLine("1. Thêm thể loại");
                Console.WriteLine("2. Xóa thể loại");
                Console.WriteLine("3. Hiển thị danh sách thể loại");
                Console.WriteLine("4. Tìm kiếm thể loại sách");
                Console.WriteLine("5. Danh sách thể loại có nhiều sách nhất");
                Console.WriteLine("6. Danh sách thể loại có ít sách nhất");
                Console.WriteLine("7. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1":
                        ThemTheLoai(context);
                        break;

                    case "2":
                        XoaTheLoai(context);
                        break;

                    case "3":
                        HienThiDanhSachTheLoai(context);
                        break;

                    case "4":
                        TimKiemTheLoai(context);
                        break;

                    case "5":
                        HienThiTheLoaiCoNhieuSachNhat(context);
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        void ThemTheLoai(ThuVienContext context)
        {
            Console.Write("Nhập tên thể loại: ");
            string tenTheLoai = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tenTheLoai))
            {
                Console.WriteLine("Tên thể loại là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var theLoai = new TheLoai { TenLoai = tenTheLoai };
            context.TheLoai.Add(theLoai);
            context.SaveChanges();
            Console.WriteLine("Thêm thể loại thành công!");
            Console.ReadKey();
        }

        void XoaTheLoai(ThuVienContext context)
        {
            Console.Write("Nhập tên thể loại cần xóa: ");
            string tenTheLoai = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tenTheLoai))
            {
                Console.WriteLine("Tên thể loại là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var theLoai = context.TheLoai.FirstOrDefault(tl => tl.TenLoai == tenTheLoai);
            if (theLoai != null)
            {
                context.TheLoai.Remove(theLoai);
                context.SaveChanges();
                Console.WriteLine("Xóa thể loại thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy thể loại với tên đã nhập.");
            }
            Console.ReadKey();
        }

        void HienThiDanhSachTheLoai(ThuVienContext context)
        {
            var theLoais = context.TheLoai.ToList();
            Console.WriteLine("Danh sách thể loại:");
            foreach (var theLoai in theLoais)
            {
                Console.WriteLine($"ID: {theLoai.Id}, Tên thể loại: {theLoai.TenLoai}");
            }
            Console.ReadKey();
        }

        void TimKiemTheLoai(ThuVienContext context)
        {
            Console.Write("Nhập tên thể loại cần tìm: ");
            string tenTheLoai = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tenTheLoai))
            {
                Console.WriteLine("Tên thể loại là bắt buộc.");
                Console.ReadKey();
                return;
            }

            var theLoaiTimThay = context.TheLoai.Where(tl => tl.TenLoai.Contains(tenTheLoai)).ToList();
            if (theLoaiTimThay.Any())
            {
                Console.WriteLine("Kết quả tìm kiếm:");
                foreach (var theLoai in theLoaiTimThay)
                {
                    Console.WriteLine($"ID: {theLoai.Id}, Tên thể loại: {theLoai.TenLoai}");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy thể loại với tên đã nhập.");
            }
            Console.ReadKey();
        }

        void HienThiTheLoaiCoNhieuSachNhat(ThuVienContext context)
        {
            var theLoaiCoNhieuSachNhat = context.Sach
                .GroupBy(s => s.TheLoai.TenLoai)
                .Select(g => new
                {
                    TheLoai = g.Key,
                    SoLuong = g.Count()
                })
                .OrderByDescending(g => g.SoLuong)
                .ToList();

            Console.WriteLine("Danh sách thể loại có nhiều sách nhất:");
            foreach (var item in theLoaiCoNhieuSachNhat)
            {
                Console.WriteLine($"Thể loại: {item.TheLoai}, Số lượng sách: {item.SoLuong}");
            }
            Console.ReadKey();
        }
    }
}
