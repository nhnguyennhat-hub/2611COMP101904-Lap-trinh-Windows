using System;

class Program
{
    static int NhapSoNguyen(string message)
    {
        int n;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out n))
            {
                return n;
            }

            Console.WriteLine("Vui long nhap so nguyen!");
        }
    }

    static int NhapSoNguyenDuong(string message)
    {
        int n;

        while (true)
        {
            n = NhapSoNguyen(message);

            if (n > 0)
            {
                return n;
            }

            Console.WriteLine("Vui long nhap so nguyen duong!");
        }
    }

    static int[] NhapMang()
    {
        int n = NhapSoNguyenDuong("Nhap so luong phan tu n: ");

        int[] a = new int[n];

        for (int i = 0; i < n; i++)
        {
            a[i] = NhapSoNguyen("Nhap a[" + i + "]: ");
        }

        return a;
    }

    static void XuatMang(int[] a)
    {
        Console.Write("Mang: ");

        for (int i = 0; i < a.Length; i++)
        {
            Console.Write(a[i] + " ");
        }

        Console.WriteLine();
    }

    static int TinhTong(int[] a)
    {
        int tong = 0;

        for (int i = 0; i < a.Length; i++)
        {
            tong += a[i];
        }

        return tong;
    }

    static int TimMax(int[] a)
    {
        int max = a[0];

        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] > max)
            {
                max = a[i];
            }
        }

        return max;
    }

    static int TimMin(int[] a)
    {
        int min = a[0];

        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] < min)
            {
                min = a[i];
            }
        }

        return min;
    }

    static int DemChan(int[] a)
    {
        int dem = 0;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] % 2 == 0)
            {
                dem++;
            }
        }

        return dem;
    }

    static int DemLe(int[] a)
    {
        int dem = 0;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] % 2 != 0)
            {
                dem++;
            }
        }

        return dem;
    }

    static void SapXepTangDan(int[] a)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[i] > a[j])
                {
                    int temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }
        }
    }

    static int TimKiem(int[] a, int x)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == x)
            {
                return i;
            }
        }

        return -1;
    }

    static void Main()
    {
        int[] a = null;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1. Nhap mang");
            Console.WriteLine("2. Xuat mang");
            Console.WriteLine("3. Tinh tong");
            Console.WriteLine("4. Tim max/min");
            Console.WriteLine("5. Dem chan/le");
            Console.WriteLine("6. Sap xep tang dan");
            Console.WriteLine("7. Tim kiem");
            Console.WriteLine("0. Thoat");

            int chon = NhapSoNguyen("Chon chuc nang: ");

            switch (chon)
            {
                case 1:
                    a = NhapMang();
                    Console.WriteLine("Nhap mang thanh cong!");
                    break;

                case 2:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }

                    XuatMang(a);
                    break;

                case 3:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }

                    Console.WriteLine("Tong = " + TinhTong(a));
                    break;

                case 4:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }

                    Console.WriteLine("Max = " + TimMax(a));
                    Console.WriteLine("Min = " + TimMin(a));
                    break;

                case 5:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }

                    Console.WriteLine("So phan tu chan = " + DemChan(a));
                    Console.WriteLine("So phan tu le = " + DemLe(a));
                    break;

                case 6:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }

                    SapXepTangDan(a);
                    Console.WriteLine("Mang sau khi sap xep:");
                    XuatMang(a);
                    break;

                case 7:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }

                    int x = NhapSoNguyen("Nhap x can tim: ");
                    int viTri = TimKiem(a, x);

                    if (viTri != -1)
                    {
                        Console.WriteLine("Tim thay " + x + " tai vi tri " + viTri);
                    }
                    else
                    {
                        Console.WriteLine("Khong tim thay " + x);
                    }

                    break;

                case 0:
                    Console.WriteLine("Ket thuc chuong trinh!");
                    return;

                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }
    }
}