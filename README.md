Oganic Store
Veefe Store là một cửa hàng trực tuyến nơi bạn có thể mua sắm các sản phẩm tươi ngon và chất lượng. Dự án này được phát triển bằng React.js cho frontend và ASP.NET Core cho backend.

Giới thiệu
Veefe Store cung cấp một nền tảng trực tuyến cho người dùng mua sắm các loại rau củ quả và các sản phẩm tươi sống khác. Người dùng có thể duyệt qua danh mục sản phẩm, thêm sản phẩm vào giỏ hàng, và thanh toán với nhiều phương thức thanh toán khác nhau.

Tính năng
Duyệt sản phẩm: Xem danh sách các sản phẩm có sẵn và các chi tiết liên quan.
Giỏ hàng: Thêm, xóa và cập nhật sản phẩm trong giỏ hàng.
Thanh toán: Hỗ trợ nhiều phương thức thanh toán.
Quản lý tài khoản: Đăng ký, đăng nhập, và quản lý thông tin cá nhân.
Quản lý đơn hàng: Theo dõi và quản lý đơn hàng.
Công nghệ sử dụng
Frontend
React.js: Thư viện JavaScript để xây dựng giao diện người dùng.
React Bootstrap: Thư viện giao diện người dùng dựa trên Bootstrap cho React.
Axios: Thư viện để thực hiện các yêu cầu HTTP.
Backend
ASP.NET Core: Framework để xây dựng các ứng dụng web và API.
Entity Framework Core: ORM để làm việc với cơ sở dữ liệu.
SQL Server: Cơ sở dữ liệu được sử dụng để lưu trữ dữ liệu.
Khác
VNPay: Dịch vụ thanh toán trực tuyến được sử dụng để xử lý các giao dịch.
Cài đặt
Yêu cầu hệ thống
Node.js
.NET Core SDK
SQL Server
Hướng dẫn cài đặt
Clone repo này:

bash
Sao chép mã
git clone https://github.com/username/veefe-store.git
Cài đặt các gói phụ thuộc:

bash
Sao chép mã
cd veefe-store/frontend
npm install
bash
Sao chép mã
cd veefe-store/backend
dotnet restore
Cấu hình cơ sở dữ liệu:

Cập nhật chuỗi kết nối trong appsettings.json để kết nối với cơ sở dữ liệu SQL Server của bạn.

Chạy ứng dụng:

Backend:

bash
Sao chép mã
cd veefe-store/backend
dotnet run
Frontend:

bash
Sao chép mã
cd Oganic-Store/frontend
npm start
Tác giả
Hồ Trương Huệ Nhật (Frontend)
Phạm Ngọc Hưng (Backend)

License
Dự án này được cấp phép theo Giấy phép MIT. Xem tệp LICENSE để biết thêm chi tiết.
