using QuanLyThuVien.Models;
using System;
using System.Linq;
using System.Text;

namespace QuanLyThuVien.Views
{
    class QuanLySach
    {
        public void QuanLySachMenu()
        {
            var context = new ThuVienContext();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nQuản lý sách:");
                Console.WriteLine("1. Thêm sách");
                Console.WriteLine("2. Xóa sách");
                Console.WriteLine("3. Hiển thị danh sách sách");
                Console.WriteLine("4. Tìm sách theo tiêu đề");
                Console.WriteLine("5. Sắp xếp sách theo số lượng");
                Console.WriteLine("6. Sắp xếp sách theo bảng chữ cái");
                Console.WriteLine("7. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1":
                        ThemSach(context);
                        break;

                    case "2":
                        XoaSach(context);
                        break;

                    case "3":
                        HienThiDanhSachSach(context);
                        break;

                    case "4":
                        TimSachTheoTieuDe(context);
                        break;

                    case "5":
                        SapXepSachTheoSoLuong(context);
                        break;

                    case "6":
                        SapXepSachTheoBangChuCai(context);
                        break;

                    case "7":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        private void ThemSach(ThuVienContext context)
        {
            Console.Write("Nhập tên sách: ");
            string tieuDe = Console.ReadLine();
            Console.Write("Nhập tác giả: ");
            string tacGia = Console.ReadLine();
            Console.Write("Nhập năm xuất bản: ");
            if (!int.TryParse(Console.ReadLine(), out int namXuatBan))
            {
                Console.WriteLine("Năm xuất bản không hợp lệ.");
                return;
            }
            Console.Write("Nhập tên thể loại: ");
            string tenTheLoai = Console.ReadLine();
            var theLoai = context.TheLoai.FirstOrDefault(t => t.TenLoai == tenTheLoai);
            if (theLoai == null)
            {
                Console.WriteLine("Không tìm thấy thể loại với tên đã nhập.");
                return;
            }
            
            Console.Write("Nhập số lượng: ");
            if (!int.TryParse(Console.ReadLine(), out int soLuong))
            {
                Console.WriteLine("Số lượng không hợp lệ.");
                return;
            }

            var sach = new Sach { TenSach = tieuDe, TacGia = tacGia, NamXuatBan = namXuatBan, TheLoaiId = theLoai.Id, SoLuong = soLuong };
            context.Sach.Add(sach);
            context.SaveChanges();
            Console.WriteLine("Thêm sách thành công!");
            Console.ReadKey();
        }

        private void XoaSach(ThuVienContext context) 
        {
            Console.Write("Nhập tên sách cần xóa: ");
            string tenSach = Console.ReadLine();
            var sach = context.Sach.FirstOrDefault(s => s.TenSach == tenSach);
            if (sach != null)
            {
                context.Sach.Remove(sach);
                context.SaveChanges();
                Console.WriteLine("Xóa sách thành công!");
            }
            else {
                Console.WriteLine("Không tìm thấy sách với tên đã nhập.");
            }
            Console.ReadKey();
        }

        private void HienThiDanhSachSach(ThuVienContext context)
        {
            var danhSachSach = context.Sach.ToList();
            Console.WriteLine("Danh sách sách:");
            foreach (var sach in danhSachSach)
            {
                var theLoai = context.TheLoai.Find(sach.TheLoaiId);
                Console.WriteLine($"ID: {sach.Id}, Tiêu đề: {sach.TenSach}, Tác giả: {sach.TacGia}, Năm xuất bản: {sach.NamXuatBan}, Thể loại: {theLoai?.TenLoai}, Số lượng: {sach.SoLuong}");
            }
            Console.ReadKey();
        }

        private void TimSachTheoTieuDe(ThuVienContext context)
        {
            Console.Write("Nhập tiêu đề sách cần tìm: ");
            string tieuDe = Console.ReadLine();
            var sachTimThay = context.Sach.Where(s => s.TenSach.Contains(tieuDe)).ToList();
            if (sachTimThay.Any())
            {
                Console.WriteLine("Kết quả tìm kiếm:");
                foreach (var sach in sachTimThay)
                {
                    var theLoai = context.TheLoai.Find(sach.TheLoaiId);
                    Console.WriteLine($"ID: {sach.Id}, Tiêu đề: {sach.TenSach}, Tác giả: {sach.TacGia}, Năm xuất bản: {sach.NamXuatBan}, Thể loại: {theLoai?.TenLoai}, Số lượng: {sach.SoLuong}");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy sách với tiêu đề đã nhập.");
            }
            Console.ReadKey();
        }

        private void SapXepSachTheoSoLuong(ThuVienContext context)
        {
            var danhSachSach = context.Sach.OrderBy(s => s.SoLuong).ToList();
            Console.WriteLine("Danh sách sách sắp xếp theo số lượng:");
            foreach (var sach in danhSachSach)
            {
                var theLoai = context.TheLoai.Find(sach.TheLoaiId);
                Console.WriteLine($"ID: {sach.Id}, Tiêu đề: {sach.TenSach}, Tác giả: {sach.TacGia}, Năm xuất bản: {sach.NamXuatBan}, Thể loại: {theLoai?.TenLoai}, Số lượng: {sach.SoLuong}");
            }
            Console.ReadKey();
        }

        private void SapXepSachTheoBangChuCai(ThuVienContext context)
        {
            var danhSachSach = context.Sach.OrderBy(s => s.TenSach).ToList();
            Console.WriteLine("Danh sách sách sắp xếp theo bảng chữ cái:");
            foreach (var sach in danhSachSach)
            {
                var theLoai = context.TheLoai.Find(sach.TheLoaiId);
                Console.WriteLine($"ID: {sach.Id}, Tiêu đề: {sach.TenSach}, Tác giả: {sach.TacGia}, Năm xuất bản: {sach.NamXuatBan}, Thể loại: {theLoai?.TenLoai}, Số lượng: {sach.SoLuong}");
            }
            Console.ReadKey();
        }
    }
}
