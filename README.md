# Hướng dẫn chạy API quản lý sách .NET 6

## Hướng dẫn chi tiết để thiết lập và chạy **API quản lý sách** được xây dựng bằng **.NET 6** trên máy tính của bạn.

---

### 1. **Yêu cầu hệ thống**
Trước khi chạy dự án, hãy đảm bảo rằng bạn đã cài đặt các công cụ sau:
- **.NET 6 SDK**: [Tải tại đây](https://dotnet.microsoft.com/download/dotnet/6.0)
- **SQL Server**: Một instance SQL Server đang chạy (ví dụ: SQL Server Express, Docker hoặc Azure SQL).
- **Git**: [Tải Git](https://git-scm.com/downloads)
- **Postman** hoặc các công cụ test API khác (tùy chọn, dùng để kiểm tra các endpoint).

---

### 2. **Clone dự án**
Dùng Git để clone repository về máy tính của bạn:
```bash
https://github.com/yamakazeAnkk/Book-store-be.git
```
Di chuyển vào thư mục dự án:
```bash
cd BookStore
```
### 3. Khôi phục thư viện
```bash
dotnet restore
```
### 4. Cấu hình kết nối
Chỉnh sửa file appsettings.json để cấu hình chuỗi kết nối với database:
```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=TÊN_SERVER;Database=BookstoreDB;Trusted_Connection=True;"
}
```
Thay TÊN_SERVER bằng tên server SQL của bạn.
### 5. Áp dụng migrations
Sau khi tạo migration, áp dụng các thay đổi vào cơ sở dữ liệu bằng lệnh:
```bash
   dotnet ef migrations add <TênMigration>
```
Áp dụng các thay đổi vào cơ sở dữ liệu:
```bash
dotnet ef database update
```
### 6. Chạy API
Khởi động ứng dụng bằng lệnh:
```bash
dotnet build
dotnet run
```
API sẽ chạy tại:
Địa chỉ chính: http://localhost:7274 (port mặc định)
 ```Plain Text
BookStore/
├── Controllers/      # Chứa các controller xử lý yêu cầu API
├── DTOs/             # Chứa các Data Transfer Objects (DTO) để trao đổi dữ liệu
├── Data/             # Chứa các lớp và cấu hình liên quan đến database
├── Helper/           # Chứa các helper class và phương thức hỗ trợ
├── Migrations/       # Chứa các file migration được tạo bởi Entity Framework
├── Models/           # Chứa các model đại diện cho dữ liệu trong ứng dụng
├── Properties/       # Chứa các file cấu hình dự án
├── Repositories/     # Chứa các lớp thực hiện các tác vụ truy vấn cơ sở dữ liệu
├── Services/         # Chứa các lớp xử lý logic nghiệp vụ
├── bin/
│   └── Debug/
│       └── net6.0/   # Thư mục build dự án ở chế độ debug (tự động sinh)
└── obj/              # Thư mục tạm tự động sinh khi biên dịch

```

# Admin's web
## Mô tả
"Đây là một website được xây dựng bằng thư viện Reactjs của JavaScript mã nguồn mở sử dụng các thư viện như Material UI, React Icons và framework Bootstrap cùng với đó là CSS
để xây dựng giao diện người dùng thân thiện và dễ sử dụng dành cho tài khoản quản trị của dự án."

## Yêu Cầu Hệ Thống
Trước khi chạy dự án, đảm bảo bạn đã cài đặt:  
- Nodejs phiên bản 20.9.0 : https://nodejs.org/en/blog/release/v20.9.0
- IDE: Visual Studio Code 

## Cài Đặt Dự Án
### 1. Clone Repository:
Clone mã nguồn từ GitHub:
```
git clone https://github.com/DevThanhLe/admin-bookstore-dashboard.git
```
### 2. Di chuyển vào thư mục dự án:
```
cd admin-bookstore-dashboard
```
### 3. Cài đặt các gói phụ thuộc (dependencies) từ trong tệp package.json:
```
npm install
```
### 4. Khởi chạy dưới dạng chế độ phát triển:
```
npm start
```
### 5. Khởi chạy dưới dạng chế độ xem và tương tác:
```
npm test
```
# Flutter Book_store 

Yêu cầu hệ thống
Trước khi chạy ứng dụng, hãy đảm bảo rằng các yêu cầu sau đã được cài đặt trên máy tính của bạn:

Flutter SDK: Tải xuống và cài đặt Flutter từ flutter.dev.
Dart SDK: Được cài đặt cùng với Flutter SDK.
Android Studio hoặc VS Code: Để phát triển và chạy ứng dụng.
Device Emulator hoặc Thiết bị vật lý: Để kiểm tra ứng dụng.
Git: Để quản lý mã nguồn.


Cách chạy ứng dụng
1. Cài đặt Flutter
Làm theo hướng dẫn tại Flutter Install để cài đặt Flutter SDK.
Đảm bảo cấu hình biến môi trường cho flutter (thêm đường dẫn Flutter SDK vào $PATH).

2. kiểm tra cấu hình môi trường - flutter doctor

3. clone repositoty 

4. cài đặt các dependencies - flutter pub get

5. Nếu chạy bằng web - Vào file api_config.dart đổi baseUrl = 'http://localhost:7274/api';

6. Nếu chạy bằng máy ảo - Vào file api_config.dart đổi baseUrl = 'http://10.0.2.2:7274/api';

Lệnh hữu ích khác
Build ứng dụng:
Android APK: - flutter build apk --release


iOS App: - flutter build ios --release

Xóa bộ nhớ cache: - flutter clean


Kiểm tra lỗi định dạng code: - flutter analyze

For help getting started with Flutter development, view the
[online documentation](https://docs.flutter.dev/), which offers tutorials,
samples, guidance on mobile development, and a full API reference.
