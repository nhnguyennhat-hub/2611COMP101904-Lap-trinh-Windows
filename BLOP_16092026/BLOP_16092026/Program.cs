using System;
using System.Collections.Generic;

class NhanVien
{
    public string MaNhanVien { get; set; }
    public string HoTen { get; set; }
    public double LuongCoBan { get; set; }

    public NhanVien(string maNhanVien, string hoTen, double luongCoBan)
    {
        MaNhanVien = maNhanVien;
        HoTen = hoTen;
        LuongCoBan = luongCoBan;
    }

    public virtual double TinhLuong()
    {
        return LuongCoBan;
    }

    public virtual void HienThiThongTin()
    {
        Console.WriteLine("Ma nhan vien: " + MaNhanVien);
        Console.WriteLine("Ho ten: " + HoTen);
        Console.WriteLine("Luong co ban: " + LuongCoBan);
        Console.WriteLine("Luong: " + TinhLuong());
    }
}


class NhanVienVanPhong : NhanVien
{
    public int SoNgayLamViec { get; set; }

    public NhanVienVanPhong(
        string maNhanVien,
        string hoTen,
        double luongCoBan,
        int soNgayLamViec)
        : base(maNhanVien, hoTen, luongCoBan)
    {
        SoNgayLamViec = soNgayLamViec;
    }

    public override double TinhLuong()
    {
        return LuongCoBan + SoNgayLamViec * 200000;
    }

    public override void HienThiThongTin()
    {
        Console.WriteLine("Loai: Nhan vien van phong");
        Console.WriteLine("Ma nhan vien: " + MaNhanVien);
        Console.WriteLine("Ho ten: " + HoTen);
        Console.WriteLine("Luong co ban: " + LuongCoBan);
        Console.WriteLine("So ngay lam viec: " + SoNgayLamViec);
        Console.WriteLine("Luong: " + TinhLuong());
    }
}


class NhanVienKinhDoanh : NhanVien
{
    public double DoanhSo { get; set; }

    public NhanVienKinhDoanh(
        string maNhanVien,
        string hoTen,
        double luongCoBan,
        double doanhSo)
        : base(maNhanVien, hoTen, luongCoBan)
    {
        DoanhSo = doanhSo;
    }

    public override double TinhLuong()
    {
        return LuongCoBan + 0.05 * DoanhSo;
    }

    public override void HienThiThongTin()
    {
        Console.WriteLine("Loai: Nhan vien kinh doanh");
        Console.WriteLine("Ma nhan vien: " + MaNhanVien);
        Console.WriteLine("Ho ten: " + HoTen);
        Console.WriteLine("Luong co ban: " + LuongCoBan);
        Console.WriteLine("Doanh so: " + DoanhSo);
        Console.WriteLine("Luong: " + TinhLuong());
    }
}


class Program
{
    static List<NhanVien> danhSach = new List<NhanVien>();


    static double NhapSoDuong(string message)
    {
        double x;

        while (true)
        {
            Console.Write(message);

            if (double.TryParse(Console.ReadLine(), out x) && x > 0)
            {
                return x;
            }

            Console.WriteLine("Vui long nhap so lon hon 0!");
        }
    }


    static int NhapSoNgayLamViec()
    {
        int soNgay;

        while (true)
        {
            Console.Write("Nhap so ngay lam viec (0-31): ");

            if (int.TryParse(Console.ReadLine(), out soNgay)
                && soNgay >= 0
                && soNgay <= 31)
            {
                return soNgay;
            }

            Console.WriteLine("So ngay phai tu 0 den 31!");
        }
    }


    static double NhapDoanhSo()
    {
        double doanhSo;

        while (true)
        {
            Console.Write("Nhap doanh so (>= 0): ");

            if (double.TryParse(Console.ReadLine(), out doanhSo)
                && doanhSo >= 0)
            {
                return doanhSo;
            }

            Console.WriteLine("Doanh so phai >= 0!");
        }
    }


    static void NhapNhanVien()
    {
        Console.WriteLine();
        Console.WriteLine("===== NHAP NHAN VIEN =====");

        Console.Write("Nhap ma nhan vien: ");
        string ma = Console.ReadLine();

        Console.Write("Nhap ho ten: ");
        string hoTen = Console.ReadLine();

        double luongCoBan = NhapSoDuong("Nhap luong co ban: ");

        Console.WriteLine();
        Console.WriteLine("1. Nhan vien van phong");
        Console.WriteLine("2. Nhan vien kinh doanh");

        int loai;

        while (true)
        {
            Console.Write("Chon loai nhan vien: ");

            if (int.TryParse(Console.ReadLine(), out loai)
                && (loai == 1 || loai == 2))
            {
                break;
            }

            Console.WriteLine("Vui long chon 1 hoac 2!");
        }

        if (loai == 1)
        {
            int soNgay = NhapSoNgayLamViec();

            NhanVienVanPhong nv =
                new NhanVienVanPhong(ma, hoTen, luongCoBan, soNgay);

            danhSach.Add(nv);
        }
        else
        {
            double doanhSo = NhapDoanhSo();

            NhanVienKinhDoanh nv =
                new NhanVienKinhDoanh(ma, hoTen, luongCoBan, doanhSo);

            danhSach.Add(nv);
        }

        Console.WriteLine("Them nhan vien thanh cong!");
    }


    static void XuatDanhSach()
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach dang rong!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== DANH SACH NHAN VIEN =====");

        foreach (NhanVien nv in danhSach)
        {
            nv.HienThiThongTin();
            Console.WriteLine("----------------------------");
        }
    }


    static void TimTheoMa()
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach dang rong!");
            return;
        }

        Console.Write("Nhap ma nhan vien can tim: ");
        string ma = Console.ReadLine();

        foreach (NhanVien nv in danhSach)
        {
            if (nv.MaNhanVien == ma)
            {
                Console.WriteLine();
                nv.HienThiThongTin();
                return;
            }
        }

        Console.WriteLine("Khong tim thay nhan vien!");
    }


    static void TimLuongCaoNhat()
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach dang rong!");
            return;
        }

        NhanVien nvMax = danhSach[0];

        foreach (NhanVien nv in danhSach)
        {
            if (nv.TinhLuong() > nvMax.TinhLuong())
            {
                nvMax = nv;
            }
        }

        Console.WriteLine();
        Console.WriteLine("===== NHAN VIEN CO LUONG CAO NHAT =====");
        nvMax.HienThiThongTin();
    }


    static void TinhTongLuong()
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach dang rong!");
            return;
        }

        double tong = 0;

        foreach (NhanVien nv in danhSach)
        {
            tong += nv.TinhLuong();
        }

        Console.WriteLine("Tong luong cong ty phai tra: " + tong);
    }


    static void HienThiMenu()
    {
        Console.WriteLine();
        Console.WriteLine("========== MENU ==========");
        Console.WriteLine("1. Xuat danh sach nhan vien");
        Console.WriteLine("2. Tim nhan vien theo ma");
        Console.WriteLine("3. Tim nhan vien co luong cao nhat");
        Console.WriteLine("4. Tinh tong luong cong ty phai tra");
        Console.WriteLine("0. Thoat");
        Console.WriteLine("==========================");
    }


    static void Main()
    {
        // Nhap it nhat 5 nhan vien
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine();
            Console.WriteLine("Nhan vien thu " + (i + 1));
            NhapNhanVien();
        }

        while (true)
        {
            HienThiMenu();

            int chon;

            while (true)
            {
                Console.Write("Chon chuc nang: ");

                if (int.TryParse(Console.ReadLine(), out chon)
                    && chon >= 0
                    && chon <= 4)
                {
                    break;
                }

                Console.WriteLine("Vui long chon tu 0 den 4!");
            }

            switch (chon)
            {
                case 1:
                    XuatDanhSach();
                    break;

                case 2:
                    TimTheoMa();
                    break;

                case 3:
                    TimLuongCaoNhat();
                    break;

                case 4:
                    TinhTongLuong();
                    break;

                case 0:
                    Console.WriteLine("Ket thuc chuong trinh!");
                    return;
            }
        }
    }
}