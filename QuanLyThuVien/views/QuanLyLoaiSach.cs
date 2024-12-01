using QuanLyThuVien.Models;
using System;
using System.Linq;

namespace QuanLyThuVien.Views
{
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
                Console.WriteLine("5. Thoát");
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
    }
}
