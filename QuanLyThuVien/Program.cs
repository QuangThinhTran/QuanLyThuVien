using QuanLyThuVien.views;
using QuanLyThuVien.Views;
using System;
using System.Text;

namespace QuanLyThuVien
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nQuản lý thư viện:");
                Console.WriteLine("1. Quản lý thể loại sách");
                Console.WriteLine("2. Quản lý sách");
                Console.WriteLine("3. Quản lý khách hàng");
                Console.WriteLine("4. Quản lý thuê sách");
                Console.WriteLine("5. Thoát");
                Console.Write("Chọn một tùy chọn: ");
                string luaChon = Console.ReadLine();
                switch (luaChon)
                {
                    case "1":
                        var quanLyTheLoai = new QuanLyLoaiSach();
                        quanLyTheLoai.QuanLyLoaiSachMenu();
                        break;

                    case "2":
                        var quanLySach = new QuanLySach();
                        quanLySach.QuanLySachMenu();
                        break;

                    case "3":
                        var quanLyKH = new QuanLyKhachHang();
                        quanLyKH.QuanLyKhachHangMenu();
                        break;

                    case "4":
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }
    }
}
