using System;
using System.Collections.Generic;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        static QuanLySinhVien quanLy = new QuanLySinhVien();

        static void Main(string[] args)
        {
            int luaChon;

            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");

                switch (luaChon)
                {
                    case 1: ThemSinhVien(); break;
                    case 2: XuatDanhSach(quanLy.LayDanhSach()); break;
                    case 3: TimSinhVienTheoMa(); break;
                    case 4: TimSinhVienTheoTen(); break;
                    case 5: SuaDiem(); break;
                    case 6: XoaSinhVien(); break;
                    case 7: XuatDanhSach(quanLy.SapXepTheoDiem()); break;
                    case 8: XuatDanhSach(quanLy.LaySinhVienDat()); break;
                    case 0: Console.WriteLine("Đã thoát chương trình."); break;
                    default: Console.WriteLine("Chức năng không hợp lệ."); break;
                }

                if (luaChon != 0)
                {
                    Console.WriteLine("\nNhấn Enter để tiếp tục...");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (luaChon != 0);
        }

        static void HienThiMenu()
        {
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
        }

        static void ThemSinhVien()
        {
            Console.WriteLine("\n--- THEM SINH VIEN ---");
            string maSinhVien = NhapChuoi("Nhập mã sinh viên: ");

            if (quanLy.TimTheoMa(maSinhVien) != null)
            {
                Console.WriteLine("Mã sinh viên đã tồn tại.");
                return;
            }

            string hoTen = NhapChuoi("Nhập họ tên: ");
            DateTime ngaySinh = NhapNgaySinh();
            string maLop = NhapChuoi("Nhập mã lớp: ");
            double diem = NhapDiem();

            SinhVien sinhVien = new SinhVien(
                maSinhVien, hoTen, ngaySinh, maLop, diem);

            if (quanLy.Them(sinhVien))
                Console.WriteLine("Thêm sinh viên thành công.");
        }

        static void TimSinhVienTheoMa()
        {
            Console.WriteLine("\n--- TIM THEO MA ---");
            string ma = NhapChuoi("Nhập mã sinh viên: ");
            SinhVien sinhVien = quanLy.TimTheoMa(ma);

            if (sinhVien == null)
                Console.WriteLine("Không tìm thấy sinh viên.");
            else
                Console.WriteLine(sinhVien.LayThongTin());
        }

        static void TimSinhVienTheoTen()
        {
            Console.WriteLine("\n--- TIM THEO TEN ---");
            string tuKhoa = NhapChuoi("Nhập từ khóa họ tên: ");
            List<SinhVien> ketQua = quanLy.TimTheoTen(tuKhoa);

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            XuatDanhSach(ketQua);
        }

        static void SuaDiem()
        {
            Console.WriteLine("\n--- SUA DIEM ---");
            string ma = NhapChuoi("Nhập mã sinh viên: ");

            if (quanLy.TimTheoMa(ma) == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            double diemMoi = NhapDiem();

            try
            {
                if (quanLy.Sua(ma, diemMoi))
                    Console.WriteLine("Cập nhật điểm thành công.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void XoaSinhVien()
        {
            Console.WriteLine("\n--- XOA SINH VIEN ---");
            string ma = NhapChuoi("Nhập mã sinh viên: ");

            if (quanLy.Xoa(ma))
                Console.WriteLine("Xóa sinh viên thành công.");
            else
                Console.WriteLine("Không tìm thấy sinh viên.");
        }

        static void XuatDanhSach(List<SinhVien> danhSach)
        {
            Console.WriteLine("\n--- DANH SACH SINH VIEN ---");

            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách đang trống.");
                return;
            }

            foreach (SinhVien sinhVien in danhSach)
                Console.WriteLine(sinhVien.LayThongTin());
        }

        static string NhapChuoi(string thongBao)
        {
            string ketQua;

            do
            {
                Console.Write(thongBao);
                ketQua = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ketQua))
                    Console.WriteLine("Không được để trống.");
            }
            while (string.IsNullOrWhiteSpace(ketQua));

            return ketQua.Trim();
        }

        static int NhapSoNguyen(string thongBao)
        {
            int ketQua;

            while (true)
            {
                Console.Write(thongBao);

                if (int.TryParse(Console.ReadLine(), out ketQua))
                    return ketQua;

                Console.WriteLine("Vui lòng nhập số nguyên.");
            }
        }

        static double NhapDiem()
        {
            double diem;

            while (true)
            {
                Console.Write("Nhập điểm trung bình: ");

                if (double.TryParse(Console.ReadLine(), out diem) &&
                    diem >= 0 && diem <= 10)
                    return diem;

                Console.WriteLine("Điểm không hợp lệ. Vui lòng nhập từ 0 đến 10.");
            }
        }

        static DateTime NhapNgaySinh()
        {
            DateTime ngaySinh;

            while (true)
            {
                Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");

                if (DateTime.TryParse(Console.ReadLine(), out ngaySinh))
                    return ngaySinh;

                Console.WriteLine("Ngày sinh không hợp lệ.");
            }
        }
    }
}
