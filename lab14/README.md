# COMP1019 – Lab 14: Entity Framework Core và mô hình ba lớp (EduManager – Quản lý môn học)

## Chạy chương trình
1. Trong SSMS chạy `Database/EduManagerDB_MonHoc.sql` (tạo DB/bảng nếu chưa có, thêm 5 môn mẫu).
2. Mở `EduManager.csproj` bằng Visual Studio 2022 → F5 (hoặc `dotnet run`). Package `Microsoft.EntityFrameworkCore.SqlServer` tự khôi phục khi build.
3. Chuỗi kết nối nằm trong `Data/EduManagerContext.cs` (mặc định `(localdb)\MSSQLLocalDB`).

**Lưu ý nếu đã làm Lab 10 trong cùng `EduManagerDB`:** Lab 10 đã có bảng `MONHOC` với cột khác (`MaMH`, `TenMH`). Script sẽ tự đổi tên bảng cũ thành `MONHOC_Lab10` (dữ liệu và khóa ngoại với KETQUA vẫn giữ nguyên) rồi tạo bảng `MONHOC` mới theo đề Lab 14. Các truy vấn Lab 10 dùng `MONHOC` sẽ cần đổi sang `MONHOC_Lab10`. Muốn tách hẳn, đổi `EduManagerDB` thành tên khác (ví dụ `EduManagerDB_Lab14`) trong script và trong `EduManagerContext.cs`.

## Cấu trúc ba lớp
```
EduManager\
├── Models\MonHoc.cs               Entity
├── Data\EduManagerContext.cs      DbContext (EF Core)
├── DAL\MonHocDAL.cs               Truy cập DB bằng EF Core, không nghiệp vụ
├── BLL\MonHocBLL.cs               Validation + quy tắc, dịch lỗi thành thông báo thân thiện
├── GUI\FrmMonHoc.cs               Nhận dữ liệu, gọi BLL, hiển thị
├── Database\EduManagerDB_MonHoc.sql
└── Program.cs
```
Luồng: `GUI → BLL → DAL → EF Core → SQL Server`. GUI không biết SQL/EF; DAL không biết giao diện.

## Validation (đặt ở BLL)
Mã/tên không rỗng, mã ≤ 10 ký tự và không chứa khoảng trắng, tên ≤ 100 ký tự, số tín chỉ 1–6, không trùng mã khi thêm (kiểm tra trước + bắt lỗi trùng khóa 2627/2601 nếu hai người thêm cùng lúc). GUI xác nhận xóa bằng MessageBox Yes/No. Xóa môn đang được bảng khác tham chiếu (lỗi 547) → thông báo thân thiện.

## Test case
| # | Thao tác | Kết quả mong đợi |
|---|---|---|
| 1 | Mở form | Lưới hiện 5 môn mẫu, thanh trạng thái "Tổng số: 5 môn học" |
| 2 | Thêm `COMP2001` / `Lập trình Web` / 3 tín chỉ | Thành công, lưới có 6 dòng |
| 3 | Thêm khi để trống Mã | "Mã môn học không được để trống" |
| 4 | Thêm khi để trống Tên | "Tên môn học không được để trống" |
| 5 | Thêm mã `COMP1019` (đã có) | "Mã môn học 'COMP1019' đã tồn tại" |
| 6 | Thêm mã `comp1019` (khác hoa/thường) | Vẫn báo trùng (collation không phân biệt hoa thường) |
| 7 | Nhập mã có khoảng trắng `COMP 01` | "Không được chứa khoảng trắng" |
| 8 | Click dòng trong lưới | 4 control nhập liệu hiện đúng dữ liệu; ô Mã bị khóa |
| 9 | Sửa tên, số tín chỉ, bỏ tick "Còn sử dụng" rồi Sửa | Lưới cập nhật đúng |
| 10 | Bấm Sửa/Xóa khi chưa chọn dòng | Nhắc chọn môn học |
| 11 | Xóa → chọn **No** | Không xóa |
| 12 | Xóa → chọn **Yes** | Xóa thành công, lưới giảm 1 dòng |
| 13 | Tìm `windows`, tìm `COMP`, tìm `zzz` | Lọc đúng; `zzz` báo không tìm thấy |
| 14 | Làm mới | Xóa trắng ô nhập và ô tìm kiếm, tải lại toàn bộ danh sách |
| 15 | Dừng SQL Server rồi mở form | Báo lỗi dễ hiểu, không crash |

## Câu hỏi nộp kèm
**1. Vì sao không nên viết toàn bộ code CRUD trong sự kiện Button Click?**
Vì giao diện, nghiệp vụ và truy cập dữ liệu bị trộn lẫn: code dài khó đọc, khó sửa; cùng một đoạn kiểm tra/SQL phải copy ở nhiều nơi; không thể kiểm thử logic nếu không mở form; đổi giao diện (WinForms → web) hoặc đổi cách truy cập dữ liệu (ADO.NET → EF) phải viết lại tất cả. Tách ra thì mỗi lớp có một trách nhiệm, tái sử dụng và kiểm thử được.

**2. BLL và DAL khác nhau ở điểm nào?**
DAL chỉ biết *cách lấy/lưu dữ liệu* (EF Core, câu truy vấn, SaveChanges) và không quyết định dữ liệu có hợp lệ hay không. BLL biết *quy tắc nghiệp vụ*: kiểm tra dữ liệu, kiểm tra trùng mã, quyết định có cho thao tác hay không rồi mới gọi DAL, và chuyển lỗi kỹ thuật thành thông báo cho người dùng. BLL không chứa truy vấn; DAL không chứa quy tắc nghiệp vụ.

**3. Entity Framework khác ADO.NET ở điểm nào?**
ADO.NET làm việc ở mức thấp: tự viết câu SQL, tạo `SqlCommand`/parameter, đọc `SqlDataReader` và ánh xạ từng cột vào đối tượng. EF Core là ORM: làm việc với các lớp C# (`DbSet<MonHoc>`), viết truy vấn bằng LINQ, EF tự sinh SQL (có tham số), tự ánh xạ kết quả vào đối tượng và theo dõi thay đổi để `SaveChanges`. Ít code lặp, an toàn kiểu dữ liệu, nhưng có độ trễ/sinh SQL khó kiểm soát hơn khi truy vấn phức tạp.

**4. Trong bài lab này, validation nào nên đặt ở BLL?**
Toàn bộ quy tắc nghiệp vụ: mã và tên không rỗng, giới hạn độ dài, không chứa khoảng trắng, số tín chỉ 1–6, không trùng mã. Ở GUI chỉ có ràng buộc hỗ trợ nhập (NumericUpDown giới hạn 1–6, `MaxLength`); BLL vẫn kiểm tra lại vì không được tin dữ liệu đến từ giao diện. Database có thêm CHECK/PK làm lớp bảo vệ cuối cùng.

## Ảnh chụp cần nộp
Form chính, thêm thành công, báo lỗi trùng mã, hộp thoại xác nhận xóa, kết quả tìm kiếm.
