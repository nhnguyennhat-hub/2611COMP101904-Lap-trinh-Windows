# Checklist bàn giao đồ án cuối kỳ – COMP1019

Đánh dấu `[x]` khi hoàn thành. Nên tự kiểm tra trên **một máy khác** (hoặc máy sạch) trước khi nộp.

## 1. Mã nguồn
- [ ] Project build thành công, **0 error** (Build → Rebuild Solution)
- [ ] Chạy từ đầu không lỗi, không phụ thuộc đường dẫn tuyệt đối trên máy cá nhân
- [ ] Tổ chức đúng kiến trúc yêu cầu (GUI/BLL/DAL hoặc MVP); Form không chứa SQL và logic nghiệp vụ
- [ ] Đã xóa code thừa, code bị comment, `MessageBox` dùng để debug
- [ ] Tên control, hàm, biến rõ ràng; có comment ở chỗ khó hiểu
- [ ] Đã xóa thư mục `bin/`, `obj/`, `.vs/` trước khi nén (có `.gitignore`)

## 2. Cơ sở dữ liệu (nếu có)
- [ ] Script tạo database + bảng + khóa chính/ngoại + ràng buộc
- [ ] Script dữ liệu mẫu đủ để demo ngay
- [ ] Chạy script nhiều lần không lỗi (có `IF NOT EXISTS` / `DROP ... IF EXISTS`)
- [ ] Chuỗi kết nối nằm một chỗ và có hướng dẫn đổi `Server=` trong README
- [ ] Không để mật khẩu thật trong source; tài khoản demo lưu dạng hash

## 3. Chức năng và kiểm thử
- [ ] Các chức năng bắt buộc của đề đều chạy đúng
- [ ] Validation: dữ liệu rỗng, sai kiểu, ngoài khoảng, trùng khóa
- [ ] Lỗi hiển thị bằng thông báo dễ hiểu, chương trình **không crash**
- [ ] Đã thử khi mất kết nối database
- [ ] Có file test case (≥ 5, nên ≥ 10) với kết quả thực tế đã điền
- [ ] Đã thử các thao tác biên: chuỗi dài, ký tự đặc biệt (`'`, `%`, tiếng Việt có dấu), danh sách rỗng

## 4. Tài liệu
- [ ] `README` gồm: giới thiệu, môi trường (Visual Studio, .NET), cách cài/chạy, tài khoản demo, cấu trúc thư mục
- [ ] Sơ đồ kiến trúc / ERD (hình hoặc mô tả)
- [ ] Phân công công việc từng thành viên (nếu làm nhóm)
- [ ] Trả lời câu hỏi nộp kèm (nếu đề có)
- [ ] Ảnh chụp màn hình các chức năng chính, ảnh lỗi/validation, ảnh in/xuất file (nếu có)

## 5. GitHub
- [ ] Repository có commit rõ ràng, nhiều lần (không chỉ một commit cuối)
- [ ] Đã push nhánh `main`, kiểm tra lại bằng cách **clone về thư mục mới và chạy được**
- [ ] Link repository công khai hoặc đã cấp quyền cho giảng viên
- [ ] Không commit file lớn, file tạm, thông tin nhạy cảm

## 6. Đóng gói và nộp
- [ ] Tên file nộp đúng quy định: `MSSV_HoTen_TenBai.zip`
- [ ] Giải nén thử file zip, mở và chạy được
- [ ] Nộp đúng hạn, đúng kênh (LMS/email) theo yêu cầu giảng viên

## 7. Chuẩn bị báo cáo / demo
- [ ] Kịch bản demo 5–7 phút: mở → thêm → sửa → xóa → tìm kiếm → lỗi validation → báo cáo/xuất file
- [ ] Dữ liệu demo đã được reset về trạng thái sạch
- [ ] Chuẩn bị trả lời: vì sao chọn kiến trúc này, validation đặt ở đâu, xử lý lỗi thế nào
- [ ] Có sẵn ảnh chụp/video dự phòng nếu máy demo gặp sự cố
