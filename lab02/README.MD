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

## 4. Hình ảnh minh họa

