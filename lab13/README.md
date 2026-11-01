# COMP1019 – Lab 13: Báo cáo sinh viên bằng LINQ

## Chạy chương trình
Yêu cầu: Windows, Visual Studio 2022 hoặc .NET 8 SDK. Không cần database.
1. Đặt file đúng cấu trúc (xem dưới), mở `StudentReportDemo.csproj` bằng Visual Studio (File → Open → Project/Solution).
2. Nhấn F5, hoặc chạy `dotnet run` trong thư mục project.

```
StudentReportDemo\
├── StudentReportDemo.csproj
├── Program.cs
├── Models\SinhVien.cs
└── Forms\FrmBaoCaoSinhVien.cs
```

## Chức năng và LINQ đã dùng
| Chức năng | LINQ |
|---|---|
| Nạp danh sách lớp vào cboLop | `GroupBy`, `Select`, `OrderBy` |
| Xem báo cáo (lọc lớp / kết quả / từ khóa) | `Where` (+ `Contains` không phân biệt hoa thường, dấu) |
| Sắp xếp giảm dần theo điểm | `OrderByDescending`, `ThenBy` |
| Tổng, số đạt, số không đạt, điểm TB (thanh trạng thái) | `Count`, `Average`, `Any` |
| Thống kê theo lớp (số SV, đạt, không đạt, TB, cao nhất, thấp nhất) | `GroupBy`, `Count`, `Average`, `Max`, `Min` |
| Xuất file | `Select`, `Where`, `Concat` để dựng dòng CSV/TXT |

- Hàm đã tách: `TaoDuLieuMau`, `LoadComboBox`, `XemBaoCao`, `CapNhatThongKe`, `ThongKeTheoLop`, `XuatFile`.
- Xuất file ghi đúng dữ liệu **đang hiển thị** (báo cáo đã lọc hoặc bảng thống kê), UTF-8 có BOM nên Excel đọc đúng tiếng Việt.
- Xử lý lỗi xuất file: `UnauthorizedAccessException`, `IOException` (file đang mở trong Excel).
- Tìm kiếm bỏ qua dấu: gõ `duc` vẫn ra "Hoàng Minh Đức".

## Test case
| # | Thao tác | Kết quả mong đợi |
|---|---|---|
| 1 | Mở form | Hiện 15 SV, sắp giảm dần theo điểm (SV014 9.50 đứng đầu); cboLop có 4 mục, cboKetQua có 3 mục |
| 2 | Chọn lớp `CNTT 21-01`, Xem báo cáo | Chỉ 5 SV của lớp |
| 3 | Chọn kết quả `Đạt` | Chỉ SV có điểm >= 5 (SV007 điểm 5.0 vẫn "Đạt") |
| 4 | Chọn kết quả `Không đạt` | Chỉ SV điểm < 5 (SV008 4.9 là "Không đạt") |
| 5 | Từ khóa `SV01` | Các SV mã SV010–SV015 |
| 6 | Từ khóa `duc` | SV005 Hoàng Minh Đức |
| 7 | Kết hợp lớp `Kinh tế 22-01` + `Không đạt` + từ khóa `van` | Phan Văn Minh |
| 8 | Từ khóa `zzz` | Hộp thoại "Không có sinh viên nào phù hợp", lưới rỗng, thanh trạng thái = 0 |
| 9 | Bấm Thống kê | 3 dòng lớp: số SV 5/4/6, đạt/không đạt, điểm TB, cao nhất, thấp nhất |
| 10 | Xuất file khi lưới rỗng | Cảnh báo "Không có dữ liệu để xuất" |
| 11 | Xem báo cáo → Xuất `.csv` → mở bằng Excel | Đủ cột, tiếng Việt không lỗi font, điểm dạng `8.50` |
| 12 | Xuất `.txt` | Có tiêu đề, ngày xuất, điều kiện lọc, dòng tổng kết, bảng thẳng hàng |
| 13 | Xuất ra file đang mở trong Excel | Thông báo lỗi ghi file, chương trình không crash |
| 14 | Bấm Thống kê rồi Xuất file | File chứa bảng thống kê theo lớp |
