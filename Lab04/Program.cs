using System;
using System.Collections.Generic;

class Program
{
    static string NhapChuoi(string thongBao)
    {
        while (true)
        {
            Console.Write(thongBao);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Du lieu khong duoc de trong!");
        }
    }

    static double NhapGia()
    {
        while (true)
        {
            Console.Write("Nhap don gia: ");

            if (double.TryParse(Console.ReadLine(), out double gia) && gia >= 0)
                return gia;

            Console.WriteLine("Don gia phai la so va khong duoc am!");
        }
    }

    static int NhapSoLuong()
    {
        while (true)
        {
            Console.Write("Nhap so luong: ");

            if (int.TryParse(Console.ReadLine(), out int soLuong) && soLuong >= 0)
                return soLuong;

            Console.WriteLine("So luong phai la so nguyen va khong duoc am!");
        }
    }

    static void InDanhSach(List<Product> danhSach)
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sach san pham dang rong!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== DANH SACH SAN PHAM =====");

        foreach (Product product in danhSach)
            Console.WriteLine(product);
    }

    static void Main()
    {
        ProductService service = new ProductService();

        service.ProductAdded += product =>
        {
            Console.WriteLine("EVENT: Them san pham thanh cong -> " + product.MaSP);
        };

        service.ProductRemoved += product =>
        {
            Console.WriteLine("EVENT: Xoa san pham thanh cong -> " + product.MaSP);
        };

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== PRODUCT MANAGER =====");
            Console.WriteLine("1. Them san pham");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim theo ma");
            Console.WriteLine("4. Tim theo ten");
            Console.WriteLine("5. Loc theo khoang gia");
            Console.WriteLine("6. Xoa san pham");
            Console.WriteLine("7. Tinh tong gia tri kho");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon: ");

            if (!int.TryParse(Console.ReadLine(), out int chon))
            {
                Console.WriteLine("Vui long nhap so!");
                continue;
            }

            try
            {
                switch (chon)
                {
                    case 1:
                        string ma = NhapChuoi("Nhap ma san pham: ");
                        string ten = NhapChuoi("Nhap ten san pham: ");
                        double gia = NhapGia();
                        int soLuong = NhapSoLuong();

                        service.AddProduct(new Product(ma, ten, gia, soLuong));
                        break;

                    case 2:
                        InDanhSach(service.GetAll());
                        break;

                    case 3:
                        string maTim = NhapChuoi("Nhap ma san pham can tim: ");
                        Console.WriteLine("Tim thay:");
                        Console.WriteLine(service.FindById(maTim));
                        break;

                    case 4:
                        string tuKhoa = NhapChuoi("Nhap tu khoa ten san pham: ");

                        List<Product> ketQuaTen = service.Search(
                            p => p.TenSP.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0
                        );

                        InDanhSach(ketQuaTen);
                        break;

                    case 5:
                        double giaMin;
                        double giaMax;

                        while (true)
                        {
                            Console.Write("Nhap gia nho nhat: ");

                            if (double.TryParse(Console.ReadLine(), out giaMin) && giaMin >= 0)
                                break;

                            Console.WriteLine("Gia khong hop le!");
                        }

                        while (true)
                        {
                            Console.Write("Nhap gia lon nhat: ");

                            if (double.TryParse(Console.ReadLine(), out giaMax) && giaMax >= giaMin)
                                break;

                            Console.WriteLine("Gia lon nhat phai >= gia nho nhat!");
                        }

                        Func<Product, bool> dieuKienGia =
                            p => p.Price >= giaMin && p.Price <= giaMax;

                        InDanhSach(service.Filter(dieuKienGia));
                        break;

                    case 6:
                        string maXoa = NhapChuoi("Nhap ma san pham can xoa: ");
                        service.RemoveProduct(maXoa);
                        break;

                    case 7:
                        Console.WriteLine(
                            "Tong gia tri kho = " +
                            service.TinhTongGiaTriKho().ToString("N0")
                        );
                        break;

                    case 0:
                        Console.WriteLine("Ket thuc chuong trinh!");
                        return;

                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
            catch (DuplicateProductException ex)
            {
                Console.WriteLine("LOI TRUNG MA: " + ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine("LOI KHONG TIM THAY: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("LOI DU LIEU: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("LOI: " + ex.Message);
            }
        }
    }
}
