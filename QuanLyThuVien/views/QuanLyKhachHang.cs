using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Views
{
    public class QuanLyKhachHang
    {
        public void QuanLyKhachHangMenu()
        {
            var context = new ThuVienContext();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nQuản lý khách hàng:");
                Console.WriteLine("1. Thêm khách hàng");
                Console.WriteLine("2. Xóa khách hàng");
                Console.WriteLine("3. Hiển thị danh sách khách hàng");
                Console.WriteLine("4. Tìm khách hàng theo tên");
                Console.WriteLine("5. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1":
                        ThemKhachHang(context);
                        break;

                    case "2":
                        XoaKhachHang(context);
                        break;

                    case "3":
                        HienThiDanhSachKhachHang(context);
                        break;

                    case "4":
                        TimKhachHangTheoTen(context);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        private void ThemKhachHang(ThuVienContext context)
        {
            Console.Write("Nhập tên khách hàng: ");
            string tenKhachHang = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tenKhachHang))
            {
                Console.WriteLine("Tên khách hàng là bắt buộc.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập địa chỉ: ");
            string diaChi = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                Console.WriteLine("Địa chỉ là bắt buộc.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập số điện thoại: ");
            string soDienThoai = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                Console.WriteLine("Số điện thoại là bắt buộc.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập email: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email là bắt buộc.");
                Console.ReadKey();
                return;
            }

            if (context.KhachHang.Any(k => k.Email == email))
            {
                Console.WriteLine("Email đã tồn tại.");
                Console.ReadKey();
                return;
            }

            if (context.KhachHang.Any(k => k.SoDienThoai == soDienThoai))
            {
                Console.WriteLine("Số điện thoại đã tồn tại.");
                Console.ReadKey();
                return;
            }

            var khachHang = new KhachHang { TenKhachHang = tenKhachHang, SoDienThoai = soDienThoai, Email = email };
            context.KhachHang.Add(khachHang);
            context.SaveChanges();
            Console.WriteLine("Thêm khách hàng thành công!");
            Console.ReadKey();
        }

        private void XoaKhachHang(ThuVienContext context)
        {
            Console.Write("Nhập email khách hàng cần xóa: ");
            string email = Console.ReadLine();
            var khachHang = context.KhachHang.FirstOrDefault(k => k.Email == email);
            if (khachHang != null)
            {
            context.KhachHang.Remove(khachHang);
            context.SaveChanges();
            Console.WriteLine("Xóa khách hàng thành công!");
            }
            else
            {
            Console.WriteLine("Không tìm thấy khách hàng với email đã nhập.");
            }
            Console.ReadKey();
        }

        private void HienThiDanhSachKhachHang(ThuVienContext context)
        {
            var danhSachKhachHang = context.KhachHang.ToList();
            Console.WriteLine("Danh sách khách hàng:");
            foreach (var khachHang in danhSachKhachHang)
            {
                Console.WriteLine($"ID: {khachHang.Id}, Tên: {khachHang.TenKhachHang}, Số điện thoại: {khachHang.SoDienThoai}, Email: {khachHang.Email}");
            }
            Console.ReadKey();
        }

        private void TimKhachHangTheoTen(ThuVienContext context)
        {
            Console.Write("Nhập tên khách hàng cần tìm: ");
            string tenKhachHang = Console.ReadLine();
            var khachHangTimThay = context.KhachHang.Where(k => k.TenKhachHang.Contains(tenKhachHang)).ToList();
            if (khachHangTimThay.Any())
            {
                Console.WriteLine("Kết quả tìm kiếm:");
                foreach (var khachHang in khachHangTimThay)
                {
                    Console.WriteLine($"ID: {khachHang.Id}, Tên: {khachHang.TenKhachHang}, Số điện thoại: {khachHang.SoDienThoai}, Email: {khachHang.Email}");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng với tên đã nhập.");
            }
            Console.ReadKey();
        }
    }
}
