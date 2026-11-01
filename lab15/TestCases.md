# Test case – Lab 15 (Quản lý sinh viên MVP)

Dữ liệu mẫu ban đầu: SV001–SV008 (8 sinh viên). Cột "Kết quả thực tế" tự điền khi chạy.

| ID | Chức năng | Thao tác / Dữ liệu | Kết quả mong đợi | Kết quả thực tế |
|---|---|---|---|---|
| TC01 | Load danh sách | Mở Form | Lưới hiện 8 sinh viên, trạng thái "Đã tải danh sách: 8 sinh viên." | |
| TC02 | Thêm hợp lệ | `SV009` / `Trịnh Văn Khoa` / `7.5` → Thêm | Thông báo thành công, lưới có 9 dòng, ô nhập được xóa | |
| TC03 | Thêm trùng mã | `SV001` / `Test` / `6` | Lỗi "Mã sinh viên 'SV001' đã tồn tại", không thêm | |
| TC04 | Thêm trùng mã khác hoa thường | `sv001` / `Test` / `6` | Vẫn báo trùng mã | |
| TC05 | Mã rỗng | Bỏ trống Mã SV | Lỗi "Mã sinh viên không được để trống" | |
| TC06 | Họ tên rỗng | `SV010` / (trống) / `6` | Lỗi "Họ tên không được để trống" | |
| TC07 | Điểm sai kiểu | `SV010` / `Test` / `abc` | Lỗi "Điểm phải là số" | |
| TC08 | Điểm ngoài khoảng | Điểm `11` rồi `-1` | Lỗi "Điểm phải nằm trong khoảng từ 0 đến 10" | |
| TC09 | Điểm dùng dấu phẩy | `SV010` / `Test` / `7,5` | Thêm thành công, điểm hiển thị 7.5 | |
| TC10 | Mã có khoảng trắng | `SV 11` | Lỗi "không được chứa khoảng trắng" | |
| TC11 | Xóa tồn tại (Yes) | Nhập `SV003` → Xóa → Yes | Xóa thành công, lưới giảm 1 dòng | |
| TC12 | Xóa nhưng chọn No | Nhập `SV004` → Xóa → No | Không xóa, lưới giữ nguyên | |
| TC13 | Xóa mã không tồn tại | `SV999` → Xóa | Lỗi "Không tìm thấy sinh viên có mã 'SV999'" | |
| TC14 | Xóa khi để trống mã | Để trống → Xóa | Lỗi yêu cầu nhập/chọn mã | |
| TC15 | Tìm theo tên (không dấu) | `duc` → Tìm kiếm | Chỉ còn SV005 Hoàng Minh Đức | |
| TC16 | Tìm theo một phần tên | `thanh` | Chỉ SV008 Bùi Thanh Lan | |
| TC17 | Tìm không có kết quả | `zzz` | Lưới rỗng, thông báo không tìm thấy | |
| TC18 | Chọn dòng trên lưới | Click dòng SV002 | 3 ô nhập hiện `SV002`, `Trần Thị Bình`, `7` | |
| TC19 | Làm mới | Nhập dữ liệu rồi bấm Làm mới | Xóa ô nhập, tải lại toàn bộ danh sách | |
