# ShopManagerMini - COMP1019 Buổi 7

## Tài khoản mẫu

| Username | Password | Họ tên | Vai trò |
|---|---|---|---|
| admin | 123 | Quản trị viên | Admin |
| user | 123 | Nhân viên bán hàng | User |

## Các Form
- FrmLogin: đăng nhập bằng ShowDialog().
- FrmMain: MDI Parent.
- FrmProduct: CRUD sản phẩm bằng BindingList + DataGridView.
- FrmCustomer: thêm và hiển thị khách hàng.
- FrmAbout: giới thiệu ứng dụng.

## Câu hỏi nộp kèm

### 1. SDI và MDI khác nhau như thế nào?
SDI (Single Document Interface) thường có một cửa sổ chính cho một tài liệu/chức năng, các cửa sổ hoạt động độc lập. MDI (Multiple Document Interface) có một Form cha chứa nhiều Form con bên trong.

### 2. Vì sao Form đăng nhập nên dùng ShowDialog()?
ShowDialog() mở Form đăng nhập theo kiểu modal, buộc người dùng đăng nhập hoặc thoát trước khi tiếp tục sử dụng Form chính.

### 3. Dữ liệu user được truyền từ FrmLogin sang FrmMain bằng cách nào?
FrmLogin lưu người dùng đăng nhập thành công vào property `LoggedInUser`. Program lấy property này sau khi `ShowDialog()` trả về `DialogResult.OK` rồi truyền vào constructor `FrmMain(User user)`.

### 4. Làm sao không mở trùng Form sản phẩm?
FrmMain duyệt `MdiChildren`, tìm Form có cùng kiểu `FrmProduct`. Nếu đã tồn tại thì gọi `Activate()`, nếu chưa có thì tạo Form mới.

### 5. Khi nào dùng constructor, khi nào dùng property?
Constructor phù hợp khi dữ liệu cần có ngay lúc tạo Form và thường là dữ liệu bắt buộc. Property phù hợp khi muốn truyền hoặc thay đổi dữ liệu sau khi Form đã được tạo.
