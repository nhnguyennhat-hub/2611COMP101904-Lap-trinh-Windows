COMP1019 - LAB 15: MVP, KIEM THU VA BAN GIAO DO AN
Module: Quan ly sinh vien (mo hinh MVP)
==========================================================

1. MOI TRUONG
- He dieu hanh: Windows 10/11
- Visual Studio 2022 (ban 17.8 tro len) voi workload ".NET desktop development"
- .NET 8 SDK (target: net8.0-windows), WinForms
- Khong dung database, khong can cai them package. Du lieu luu trong bo nho (List<Student>).
- Tai khoan demo: KHONG CAN (chuong trinh khong co dang nhap).

2. CACH CHAY
Cach 1 (Visual Studio): File > Open > Project/Solution > chon StudentMvp.csproj > nhan F5.
Cach 2 (dong lenh):     cd StudentMvp  roi  dotnet run

Khi mo len, danh sach da co san 8 sinh vien mau (SV001 - SV008) de kiem tra ngay.

3. CHUC NANG
- Load danh sach: nut "Load danh sach" (tu dong chay khi mo form).
- Them: nhap Ma SV, Ho ten, Diem (0-10) roi bam "Them".
- Xoa theo ma: nhap ma (hoac click mot dong tren luoi de dien san) roi bam "Xoa theo ma"; co hop thoai xac nhan.
- Tim kiem: nhap mot phan ma hoac ten (khong phan biet hoa thuong, dau tieng Viet), bam "Tim kiem" hoac Enter.
- Kiem tra: de trong, diem sai kieu (chu), diem ngoai 0-10, ma co khoang trang, trung ma (khong phan biet hoa thuong).
- Lam moi: xoa o nhap va tai lai toan bo danh sach.

4. KIEN TRUC MVP
Models\Student.cs                    Du lieu (Student).
Views\IStudentView.cs                Interface View: Presenter chi biet interface nay.
Views\FrmStudent.cs                  Form implements IStudentView; Button Click chi goi _presenter.XXX().
Presenters\StudentPresenter.cs       Toan bo logic: validate, them, xoa, tim kiem. KHONG dung System.Windows.Forms.
Repositories\IStudentRepository.cs   Hop dong luu tru.
Repositories\StudentRepository.cs    Cai dat bang List<Student> + du lieu mau.
Program.cs                           Diem vao.

Luong: Form --(goi)--> Presenter --(doc/ghi qua IStudentView)--> Form
                       Presenter --(IStudentRepository)--> du lieu
Loi ich: Presenter khong phu thuoc WinForms nen co the kiem thu bang View gia; doi kho luu tru
(List -> SQL Server) chi can viet them mot Repository moi, khong sua Presenter va Form.

5. TAI LIEU KEM THEO
Docs\TestCases.md          Test case (18 truong hop)
Docs\Checklist_BanGiao.md  Checklist ban giao do an cuoi ky
Anh chup man hinh: dat trong thu muc screenshots\
