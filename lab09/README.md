# COMP1019 – Lab 09: Thiết kế và in thẻ sinh viên (WinForms)

## Yêu cầu môi trường
- Windows 10/11, .NET 8 SDK (hoặc Visual Studio 2022 có workload ".NET desktop development").

## Cách chạy
**Visual Studio:** mở `StudentCardApp.csproj` → nhấn `F5`.
**Dòng lệnh:**
```
cd StudentCardApp
dotnet run
```

## Cách dùng
1. Nhập **Mã sinh viên**, **Họ tên** (bắt buộc), chọn Lớp, Khoa, Khóa, Ngày sinh, Giới tính.
2. Bấm **Chọn ảnh...** để chọn ảnh JPG/PNG/BMP.
3. Thẻ hiển thị ở bên phải và tự cập nhật khi dữ liệu thay đổi.
4. **Xem trước in** → mở PrintPreviewDialog; **In thẻ** → chọn máy in (PrintDialog) rồi in.
5. **Làm mới** → xóa toàn bộ dữ liệu và ảnh; **Thoát** → đóng chương trình.

## Cấu trúc
- `FrmStudentCard.cs`: Form chính (dựng UI bằng code, đặt tên control theo tiền tố `txt/cbo/dtp/pic/btn/pnl`).
- Hàm `DrawCard(...)` dùng chung cho Panel preview và `PrintDocument` → bản in giống preview.
- Ảnh được đọc vào bộ nhớ (`new Bitmap(img)`) rồi đóng stream → không khóa file ảnh.

## Test case (đã tự kiểm thử)
| # | Mô tả | Dữ liệu / thao tác | Kết quả mong đợi | Kết quả |
|---|-------|--------------------|------------------|---------|
| 1 | Mở chương trình | Chạy app | Form hiển thị, preview báo cần nhập dữ liệu | Đạt |
| 2 | Thiếu mã SV | Bỏ trống Mã SV, bấm Xem trước in | Thông báo "Vui lòng nhập Mã sinh viên", không mở preview | Đạt |
| 3 | Thiếu họ tên | Có mã SV, bỏ trống Họ tên, bấm In | Thông báo thiếu Họ tên, không mở PrintDialog | Đạt |
| 4 | Thiếu ảnh | Có mã SV + họ tên, chưa chọn ảnh, bấm Xem trước in | Thông báo chọn ảnh, không mở preview | Đạt |
| 5 | Chọn ảnh JPG | Chọn file .jpg | Ảnh hiện trong PictureBox đúng tỉ lệ | Đạt |
| 6 | Chọn ảnh PNG ngang/dọc | Chọn ảnh có tỉ lệ khác 3:4 | Ảnh không bị méo ở PictureBox và trên thẻ | Đạt |
| 7 | Đổi ảnh nhiều lần | Chọn ảnh A rồi ảnh B, xóa/đổi tên ảnh A trong Explorer | Không lỗi, file A không bị khóa | Đạt |
| 8 | Preview realtime | Gõ tên, đổi lớp/khoa/ngày sinh | Thẻ cập nhật ngay | Đạt |
| 9 | Họ tên dài | Nhập tên > 40 ký tự | Chữ bị cắt "…", không tràn khung | Đạt |
| 10 | Print Preview | Nhập đủ dữ liệu, bấm Xem trước in | Hiện thẻ có khung, tiêu đề, ảnh, thông tin giống preview | Đạt |
| 11 | In | Bấm In thẻ → chọn máy in/PDF | Thẻ được in đúng | Đạt |
| 12 | Làm mới | Nhập đủ rồi bấm Làm mới | Xóa chữ, ảnh; combobox về mục đầu; preview trở lại trạng thái ban đầu | Đạt |

## Ảnh chụp màn hình
Thêm vào thư mục `screenshots/`: `main-form.png`, `print-preview.png`.
