namespace ExamApp.Contants;

//quyền truy cập exam
//quyền sửa và xóa exam thuộc về người tạo
public enum Access
{
    PUBLIC, // tất cả đều được xem
    PRIVATE, // chỉ người tạo mới có thể xem
    IN_GROUP // những người thuộc group mới được xem
}