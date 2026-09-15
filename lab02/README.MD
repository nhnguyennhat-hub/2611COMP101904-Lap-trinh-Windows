# Bài Lab 02 - Quản lý mảng số nguyên bằng Console

## 1. Mô tả bài toán
Viết chương trình Console quản lý một mảng số nguyên. Chương trình hiển thị menu để người dùng lựa chọn chức 
năng. Sau khi thực hiện xong một chức năng, chương trình quay lại menu cho đến khi người dùng chọn thoát.

## 2. Kết quả đạt được
- Thiết kế giao diện đầy đủ control, bố cục rõ ràng.
- Xử lý các nút Hiển thị, Xóa, Thoát đúng yêu cầu.
- Bắt lỗi dữ liệu đầu vào.

## 3. Giải thich
Về hàm tìm kiếm là:

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

Hàm TimKiem dùng để tìm giá trị x trong mảng. Hàm duyệt từ đầu mảng đến cuối mảng. Nếu tìm thấy x thì trả về vị trí đầu tiên. Nếu không tìm thấy thì trả về -1.

vd:

Mảng: 4 1 9 2 7
       0 1 2 3 4

x = 9

a[0] = 4 → không
a[1] = 1 → không
a[2] = 9 → tìm thấy

return 2
## 4. Hình ảnh minh họa
![anh demo](https://github.com/nhnguyennhat-hub/2611COMP101904-Lap-trinh-Windows/blob/main/lab02/image%2002/Screenshot%202026-09-15%20230221.png)
