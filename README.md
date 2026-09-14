# BelanjaYuk

BelanjaYuk adalah aplikasi e-commerce berbasis web yang dikembangkan dengan arsitektur Client-Server. Sistem ini menyediakan alur belanja untuk Buyer mulai dari registrasi dan login, melihat produk, memilih kategori, mengelola keranjang, melakukan checkout, memilih metode pembayaran, melihat riwayat transaksi, hingga mengelola profile pengguna.

## Features

### Authentication

- Register akun Buyer
- Login menggunakan email atau nomor telepon
- Logout
- Session management

### Product & Category

- Menampilkan daftar produk
- Melihat detail produk
- Filter produk berdasarkan kategori
- Menampilkan kategori aktif

### Shopping Cart

- Menambahkan produk ke cart
- Mengubah quantity produk
- Menghapus produk dari cart
- Menghitung subtotal dan total belanja

### Checkout & Payment

- Memilih metode pembayaran
- Melakukan checkout
- Membuat transaksi berdasarkan isi cart
- Mengosongkan cart setelah checkout berhasil

### Transaction History

- Menampilkan riwayat transaksi
- Menampilkan metode pembayaran
- Menampilkan detail produk dalam transaksi
- Menampilkan quantity, harga, discount, subtotal, dan total pembayaran

### Profile

- Melihat profile pengguna
- Mengubah username
- Mengubah nama
- Mengubah email
- Mengubah nomor telepon
- Mengubah tanggal lahir
- Mengubah gender

## Technology Stack

### Backend

- ASP.NET Core Web API
- .NET 8
- Entity Framework Core
- Microsoft SQL Server

### Frontend

- ASP.NET Core MVC
- .NET 8
- Razor Views
- Bootstrap

### Database

- Microsoft SQL Server
- Database: `belanjayuk`

## System Architecture

BelanjaYuk menggunakan pendekatan Client-Server.

```text
Buyer
  ↓
WebClient (ASP.NET Core MVC)
  ↓
REST API
  ↓
Backend (ASP.NET Core Web API)
  ↓
Entity Framework Core
  ↓
Microsoft SQL Server
```

Frontend tidak mengakses database secara langsung. Seluruh proses data dilakukan melalui REST API yang disediakan oleh Backend.

## Project Structure

```text
Belanja_yuk/
├── Backend/
│   └── BelanjaYuk.API/
│       ├── Controllers/
│       ├── Data/
│       ├── Models/
│       ├── Program.cs
│       └── appsettings.json
│
├── WebClient/
│   └── BelanjaYuk.Web/
│       ├── Controllers/
│       ├── Models/
│       ├── Views/
│       ├── wwwroot/
│       ├── Program.cs
│       └── appsettings.json
│
├── Database/
│   ├── belanjayuk_schema.sql
│   └── belanjayuk_data.sql
│
├── .gitignore
└── README.md
```

## Main User Flow

```text
Register
   ↓
Login
   ↓
Products
   ↓
Category Filter
   ↓
Add to Cart
   ↓
Update / Delete Cart
   ↓
Select Payment
   ↓
Checkout
   ↓
Transaction History
   ↓
Profile
   ↓
Edit Profile
```

## API Endpoints

### Authentication

```text
POST /api/Auth/login
POST /api/Auth/register
```

### Products

```text
GET /api/Products
GET /api/Products/{id}
GET /api/Products?category={categoryId}
```

### Categories

```text
GET /api/Categories
GET /api/Categories/active
GET /api/Categories/{id}
```

### Cart

```text
GET    /api/Cart/{userId}
POST   /api/Cart
PUT    /api/Cart/{id}
DELETE /api/Cart/{id}
```

### Payment

```text
GET /api/Payments
GET /api/Payments/{id}
```

### Transaction

```text
POST /api/Transaction/checkout/{userId}
GET  /api/Transaction/user/{userId}
```

### Profile

```text
GET /api/Profile/{userId}
PUT /api/Profile/{userId}
```

## Database

BelanjaYuk menggunakan Microsoft SQL Server dengan database bernama:

```text
belanjayuk
```

File database disimpan di folder:

```text
Database/
├── belanjayuk_schema.sql
└── belanjayuk_data.sql
```

### Database Schema

File `belanjayuk_schema.sql` digunakan untuk membuat database dan struktur tabel yang dibutuhkan oleh aplikasi.

Beberapa tabel utama yang digunakan:

```text
MsUser
MsUserPassword
MsProduct
LtCategory
LtGender
LtPayment
TrBuyerCart
TrBuyerTransaction
TrBuyerTransactionDetail
```

Database juga memiliki tabel pendukung lainnya untuk kebutuhan sistem.

### Database Data

File `belanjayuk_data.sql` digunakan untuk mengisi data awal ke dalam database, seperti:

- Data kategori produk
- Data gender
- Data metode pembayaran
- Data user
- Data password user
- Data produk
- Data pendukung lainnya

### Database Setup Order

Untuk membuat database dari awal, jalankan file SQL dengan urutan berikut:

1. Jalankan `Database/belanjayuk_schema.sql`
2. Setelah database dan tabel berhasil dibuat, jalankan `Database/belanjayuk_data.sql`

Pastikan database `belanjayuk` dan seluruh tabel sudah berhasil dibuat sebelum menjalankan file data.

## How to Run

### 1. Prepare Database

Pastikan Microsoft SQL Server dan SQL Server instance yang digunakan oleh project sudah aktif.

Buka SQL Server Management Studio (SSMS), kemudian jalankan file:

```text
Database/belanjayuk_schema.sql
```

Setelah database dan tabel berhasil dibuat, jalankan:

```text
Database/belanjayuk_data.sql
```

Kedua file tersebut harus dijalankan secara berurutan agar struktur database dan data awal tersedia.

### 2. Run Backend

Masuk ke folder:

```text
Backend/BelanjaYuk.API
```

Jalankan:

```bash
dotnet restore
dotnet build
dotnet run
```

Backend berjalan pada:

```text
http://localhost:5296
```

### 3. Run WebClient

Buka terminal baru dan masuk ke folder:

```text
WebClient/BelanjaYuk.Web
```

Jalankan:

```bash
dotnet restore
dotnet build
dotnet run
```

WebClient berjalan pada:

```text
http://localhost:5165
```

Pastikan Backend dijalankan terlebih dahulu sebelum menggunakan WebClient.

### 4. Open the Application

Setelah Backend dan WebClient berhasil berjalan, buka:

```text
http://localhost:5165
```

Kemudian user dapat melakukan alur utama aplikasi:

```text
Register
   ↓
Login
   ↓
Products
   ↓
Category Filter
   ↓
Add to Cart
   ↓
Checkout
   ↓
Payment
   ↓
Transaction History
   ↓
Profile
```

## Testing

Alur utama sistem telah diuji secara end-to-end:

```text
Register
→ Login
→ Products
→ Category Filter
→ Add Cart
→ Update Cart
→ Delete Cart
→ Re-add Cart
→ Payment
→ Checkout
→ Transaction History
→ Profile
→ Edit Profile
```

Seluruh alur utama tersebut telah berhasil dijalankan.

## Scope

Versi ini berfokus pada sisi **Buyer**.

Fitur Seller/Admin tidak menjadi bagian dari user flow utama aplikasi ini. Struktur database yang memiliki tabel pendukung Seller/Admin tetap dipertahankan untuk menjaga kompatibilitas dengan database yang tersedia.

## Development Notes

Project dikembangkan menggunakan pendekatan modular dengan pemisahan antara:

- Presentation layer
- API layer
- Business logic
- Data access
- Database

Pemisahan Backend dan WebClient memungkinkan pengembangan serta pemeliharaan masing-masing komponen secara lebih terstruktur.

## Status

**Project Status: Completed**

Core Buyer e-commerce flow telah diimplementasikan dan diuji secara end-to-end.