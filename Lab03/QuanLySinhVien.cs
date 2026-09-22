using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    public class QuanLySinhVien
    {
        private List<SinhVien> danhSachSinhVien;

        public QuanLySinhVien()
        {
            danhSachSinhVien = new List<SinhVien>();
        }

        public bool Them(SinhVien sinhVien)
        {
            if (TimTheoMa(sinhVien.MaSinhVien) != null)
                return false;

            danhSachSinhVien.Add(sinhVien);
            return true;
        }

        public bool Sua(string maSinhVien, double diemMoi)
        {
            SinhVien sinhVien = TimTheoMa(maSinhVien);

            if (sinhVien == null)
                return false;

            sinhVien.DiemTrungBinh = diemMoi;
            return true;
        }

        public bool Xoa(string maSinhVien)
        {
            SinhVien sinhVien = TimTheoMa(maSinhVien);

            if (sinhVien == null)
                return false;

            danhSachSinhVien.Remove(sinhVien);
            return true;
        }

        public SinhVien TimTheoMa(string maSinhVien)
        {
            return danhSachSinhVien.FirstOrDefault(
                sv => sv.MaSinhVien.Equals(maSinhVien, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return danhSachSinhVien
                .Where(sv => sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<SinhVien> SapXepTheoDiem()
        {
            return danhSachSinhVien
                .OrderByDescending(sv => sv.DiemTrungBinh)
                .ToList();
        }

        public List<SinhVien> LayDanhSach()
        {
            return danhSachSinhVien;
        }

        public List<SinhVien> LaySinhVienDat()
        {
            return danhSachSinhVien
                .Where(sv => sv.DiemTrungBinh >= 5)
                .ToList();
        }
    }
}
