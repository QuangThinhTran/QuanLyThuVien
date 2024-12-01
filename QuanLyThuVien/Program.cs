using QuanLyThuVien.Models;
using QuanLyThuVien.Views;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace QuanLyThuVien
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                var context = new ThuVienContext();
                Console.Clear();
                Console.WriteLine("\nQuản lý thư viện:");
                Console.WriteLine("1. Hiển thị danh sách thể loại");
                Console.WriteLine("2. Tìm kiếm thể loại sách");
                Console.WriteLine("3. Hiển thị thể loại có nhiều sách nhất");
                Console.WriteLine("4. Hiển thị thể loại có ít sách nhất");
                Console.WriteLine("5. Hiển thị thể loại sách được thuê nhiều nhất");
                Console.WriteLine("6. Hiển thị thể loại sách được thuê ít nhất");
                Console.WriteLine("7. Hiển thị danh sách sách");
                Console.WriteLine("8. Tìm kiếm sách");
                Console.WriteLine("9. Hiển thị sách được thuê nhiều nhất");
                Console.WriteLine("10. Hiển thị sách được thuê ít nhất");
                Console.WriteLine("11. Sắp xếp sách theo số lượng");
                Console.WriteLine("12. Sắp xếp sách theo tên");
                Console.WriteLine("13. Hiển thị danh sách khách hàng");
                Console.WriteLine("14. Hiển thị danh sách khách hàng đã thuê sách");
                Console.WriteLine("15. Hiển thị danh sách khách hàng chưa thuê sách");
                Console.WriteLine("16. Hiển thị khách hàng thuê nhiều sách nhất");
                Console.WriteLine("17. Hiển thị khách hàng thuê ít sách nhất");
                Console.WriteLine("18. Tìm khách hàng theo tên");
                Console.WriteLine("19. Sắp xếp khách hàng theo tên");
                Console.WriteLine("20. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1":
                        HienThiDanhSachTheLoai(context);
                        break;

                    case "2":
                        TimKiemTheLoai(context);
                        break;

                    case "3":
                        HienThiTheLoaiCoNhieuSachNhat(context);
                        break;

                    case "4":
                        HienThiTheLoaiCoItSachNhat(context);
                        break;

                    case "5":
                        HienThiTheLoaiSachDuocThueNhieuNhat(context);
                        break;

                    case "6":
                        HienThiTheLoaiSachDuocThueItNhat(context);
                        break;

                    case "7":
                        HienThiDanhSachSach(context);
                        break;

                    case "8":
                        TimKiemSach(context);
                        break;

                    case "9":
                        HienThiSachDuocThueNhieuNhat(context);
                        break;

                    case "10":
                        HienThiSachDuocThueItNhat(context);
                        break;

                    case "11":
                        SapXepSachTheoSoLuong(context);
                        break;

                    case "12":
                        SapXepSachTheoTen(context);
                        break;

                    case "13":
                        HienThiDanhSachKhachHang(context);
                        break;

                    case "14":
                        HienThiDanhSachKhachHangDaThueSach(context);
                        break;

                    case "15":
                        HienThiDanhSachKhachHangChuaThueSach(context);
                        break;

                    case "16":
                        HienThiKhachHangThueNhieuSachNhat(context);
                        break;

                    case "17":
                        HienThiKhachHangThueItSachNhat(context);
                        break;

                    case "18":
                        TimKhachHangTheoTen(context);
                        break;

                    case "19":
                        SapXepKhachHangTheoTen(context);
                        break;

                    case "20":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
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
                var theLoai = context.TheLoai.Include(tl => tl.Sach).OrderByDescending(tl => tl.Sach.Count).FirstOrDefault();
                if (theLoai != null)
                {
                    Console.WriteLine($"Thể loại có nhiều sách nhất: ID: {theLoai.Id}, Tên thể loại: {theLoai.TenLoai}, Số lượng sách: {theLoai.Sach.Count}");
                }
                else
                {
                    Console.WriteLine("Không có thể loại nào.");
                }
                Console.ReadKey();
            }

            void HienThiTheLoaiCoItSachNhat(ThuVienContext context)
            {
                var theLoai = context.TheLoai.OrderBy(tl => tl.Sach.Count).FirstOrDefault();
                if (theLoai != null)
                {
                    Console.WriteLine($"Thể loại có ít sách nhất: ID: {theLoai.Id}, Tên thể loại: {theLoai.TenLoai}, Số lượng sách: {theLoai.Sach.Count}");
                }
                else
                {
                    Console.WriteLine("Không có thể loại nào.");
                }
                Console.ReadKey();
            }

            void HienThiTheLoaiSachDuocThueNhieuNhat(ThuVienContext context)
            {
                var theLoais = context.TheLoai
                    .Include(tl => tl.Sach)
                    .ThenInclude(s => s.ChiTietPhieuThue)
                    .ToList()
                    .Select(tl => new
                    {
                        TheLoai = tl,
                        TongSoLuongThue = tl.Sach.Sum(s => s.ChiTietPhieuThue.Sum(ct => ct.SoLuong))
                    })
                    .OrderByDescending(tl => tl.TongSoLuongThue)
                    .FirstOrDefault();

                if (theLoais != null)
                {
                    Console.WriteLine($"Thể loại sách được thuê nhiều nhất: ID: {theLoais.TheLoai.Id}, Tên thể loại: {theLoais.TheLoai.TenLoai}, Số lượng sách được thuê: {theLoais.TongSoLuongThue}");
                }
                else
                {
                    Console.WriteLine("Không có thể loại nào.");
                }
                Console.ReadKey();
            }

            void HienThiTheLoaiSachDuocThueItNhat(ThuVienContext context)
            {
                var theLoais = context.TheLoai
                    .Include(tl => tl.Sach)
                    .ThenInclude(s => s.ChiTietPhieuThue)
                    .ToList()
                    .Select(tl => new
                    {
                        TheLoai = tl,
                        TongSoLuongThue = tl.Sach.Sum(s => s.ChiTietPhieuThue.Sum(ct => ct.SoLuong))
                    })
                    .OrderBy(tl => tl.TongSoLuongThue)
                    .FirstOrDefault();

                if (theLoais != null)
                {
                    Console.WriteLine($"Thể loại sách được thuê ít nhất: ID: {theLoais.TheLoai.Id}, Tên thể loại: {theLoais.TheLoai.TenLoai}, Số lượng sách được thuê: {theLoais.TongSoLuongThue}");
                }
                else
                {
                    Console.WriteLine("Không có thể loại nào.");
                }
                Console.ReadKey();
            }

            void HienThiDanhSachSach(ThuVienContext context)
            {
                var sachs = context.Sach.ToList();
                Console.WriteLine("Danh sách sách:");
                foreach (var sach in sachs)
                {
                    Console.WriteLine($"ID: {sach.Id}, Tên sách: {sach.TenSach}, Số lượng: {sach.SoLuong}");
                }
                Console.ReadKey();
            }

            void TimKiemSach(ThuVienContext context)
            {
                Console.Write("Nhập tên sách cần tìm: ");
                string tenSach = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tenSach))
                {
                    Console.WriteLine("Tên sách là bắt buộc.");
                    Console.ReadKey();
                    return;
                }

                var sachTimThay = context.Sach.Where(s => s.TenSach.Contains(tenSach)).ToList();
                if (sachTimThay.Any())
                {
                    Console.WriteLine("Kết quả tìm kiếm:");
                    foreach (var sach in sachTimThay)
                    {
                        Console.WriteLine($"ID: {sach.Id}, Tên sách: {sach.TenSach}, Số lượng: {sach.SoLuong}");
                    }
                }
                else
                {
                    Console.WriteLine("Không tìm thấy sách với tên đã nhập.");
                }
                Console.ReadKey();
            }

            void HienThiSachDuocThueNhieuNhat(ThuVienContext context)
            {
                var sach = context.Sach.Include(s => s.ChiTietPhieuThue).OrderByDescending(s => s.ChiTietPhieuThue.Sum(ct => ct.SoLuong)).FirstOrDefault();
                if (sach != null)
                {
                    Console.WriteLine($"Sách được thuê nhiều nhất: ID: {sach.Id}, Tên sách: {sach.TenSach}, Số lượng thuê: {sach.ChiTietPhieuThue.Sum(ct => ct.SoLuong)}");
                }
                else
                {
                    Console.WriteLine("Không có sách nào.");
                }
                Console.ReadKey();
            }

            void HienThiSachDuocThueItNhat(ThuVienContext context)
            {
                var sach = context.Sach.Include(s => s.ChiTietPhieuThue).OrderBy(s => s.ChiTietPhieuThue.Sum(ct => ct.SoLuong)).FirstOrDefault();
                if (sach != null)
                {
                    Console.WriteLine($"Sách được thuê ít nhất: ID: {sach.Id}, Tên sách: {sach.TenSach}, Số lượng thuê: {sach.ChiTietPhieuThue.Sum(ct => ct.SoLuong)}");
                }
                else
                {
                    Console.WriteLine("Không có sách nào.");
                }
                Console.ReadKey();
            }

            void SapXepSachTheoSoLuong(ThuVienContext context)
            {
                var sachs = context.Sach.OrderBy(s => s.SoLuong).ToList();
                Console.WriteLine("Danh sách sách sắp xếp theo số lượng:");
                foreach (var sach in sachs)
                {
                    Console.WriteLine($"ID: {sach.Id}, Tên sách: {sach.TenSach}, Số lượng: {sach.SoLuong}");
                }
                Console.ReadKey();
            }

            void SapXepSachTheoTen(ThuVienContext context)
            {
                var sachs = context.Sach.OrderBy(s => s.TenSach).ToList();
                Console.WriteLine("Danh sách sách sắp xếp theo tên:");
                foreach (var sach in sachs)
                {
                    Console.WriteLine($"ID: {sach.Id}, Tên sách: {sach.TenSach}, Số lượng: {sach.SoLuong}");
                }
                Console.ReadKey();
            }

            void HienThiDanhSachKhachHang(ThuVienContext context)
            {
                var khachHangs = context.KhachHang.ToList();
                Console.WriteLine("Danh sách khách hàng:");
                foreach (var khachHang in khachHangs)
                {
                    Console.WriteLine($"ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}");
                }
                Console.ReadKey();
            }

            void HienThiDanhSachKhachHangDaThueSach(ThuVienContext context)
            {
                var khachHangs = context.KhachHang.Where(kh => kh.PhieuThue.Any(pt => pt.ChiTietPhieuThue.Any())).ToList();
                Console.WriteLine("Danh sách khách hàng đã thuê sách:");
                foreach (var khachHang in khachHangs)
                {
                    Console.WriteLine($"ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}");
                }
                Console.ReadKey();
            }

            void HienThiDanhSachKhachHangChuaThueSach(ThuVienContext context)
            {
                var khachHangs = context.KhachHang.Where(kh => !kh.PhieuThue.Any(pt => pt.ChiTietPhieuThue.Any())).ToList();
                Console.WriteLine("Danh sách khách hàng chưa thuê sách:");
                foreach (var khachHang in khachHangs)
                {
                    Console.WriteLine($"ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}");
                }
                Console.ReadKey();
            }

            void HienThiKhachHangThueNhieuSachNhat(ThuVienContext context)
            {
                var khachHang = context.KhachHang
                    .Include(kh => kh.PhieuThue)
                    .ThenInclude(pt => pt.ChiTietPhieuThue)
                    .ToList()
                    .Select(kh => new 
                    {
                        KhachHang = kh,
                        TongSoLuongThue = kh.PhieuThue.Sum(pt => pt.ChiTietPhieuThue.Sum(ct => ct.SoLuong))
                    })
                    .OrderByDescending(kh => kh.TongSoLuongThue)
                    .FirstOrDefault()?.KhachHang;
                if (khachHang != null)
                {
                    var soLuongSachThue = khachHang.PhieuThue.Sum(pt => pt.ChiTietPhieuThue.Sum(ct => ct.SoLuong));
                    Console.WriteLine($"Khách hàng thuê nhiều sách nhất: ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}, Số lượng sách thuê: {soLuongSachThue}");
                }
                else
                {
                    Console.WriteLine("Không có khách hàng nào.");
                }
                Console.ReadKey();
            }

            void HienThiKhachHangThueItSachNhat(ThuVienContext context)
            {
                var khachHang = context.KhachHang
                    .Include(kh => kh.PhieuThue)
                    .ThenInclude(pt => pt.ChiTietPhieuThue)
                    .ToList()
                    .Select(kh => new 
                    {
                        KhachHang = kh,
                        TongSoLuongThue = kh.PhieuThue.Sum(pt => pt.ChiTietPhieuThue.Sum(ct => ct.SoLuong))
                    })
                    .OrderBy(kh => kh.TongSoLuongThue)
                    .FirstOrDefault()?.KhachHang;
                if (khachHang != null)
                {
                    var soLuongSachThue = khachHang.PhieuThue.Sum(pt => pt.ChiTietPhieuThue.Sum(ct => ct.SoLuong));
                    Console.WriteLine($"Khách hàng thuê ít sách nhất: ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}, Số lượng sách thuê: {soLuongSachThue}");
                }
                else
                {
                    Console.WriteLine("Không có khách hàng nào.");
                }
                Console.ReadKey();
            }

            void TimKhachHangTheoTen(ThuVienContext context)
            {
                Console.Write("Nhập tên khách hàng cần tìm: ");
                string tenKhachHang = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tenKhachHang))
                {
                    Console.WriteLine("Tên khách hàng là bắt buộc.");
                    Console.ReadKey();
                    return;
                }

                var khachHangTimThay = context.KhachHang.Where(kh => kh.TenKhachHang.Contains(tenKhachHang)).ToList();
                if (khachHangTimThay.Any())
                {
                    Console.WriteLine("Kết quả tìm kiếm:");
                    foreach (var khachHang in khachHangTimThay)
                    {
                        Console.WriteLine($"ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}");
                    }
                }
                else
                {
                    Console.WriteLine("Không có khách hàng nào.");
                }
                Console.ReadKey();
            }

            void SapXepKhachHangTheoTen(ThuVienContext context)
            {
                var khachHangs = context.KhachHang.OrderBy(kh => kh.TenKhachHang).ToList();
                Console.WriteLine("Danh sách khách hàng sắp xếp theo tên:");
                foreach (var khachHang in khachHangs)
                {
                    Console.WriteLine($"ID: {khachHang.Id}, Tên khách hàng: {khachHang.TenKhachHang}");
                }
                Console.ReadKey();
            }
        }
    }
}
