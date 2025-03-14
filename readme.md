# To-Do List API

## Deskripsi
API CRUD untuk To-Do List menggunakan .NET Core dan MySQL.

## Teknologi
- Backend: ASP.NET Core, Dapper
- Frontend: HTML, CSS, jQuery AJAX
- Database: MySQL

## Instalasi
1. Clone repositori ini.
2. Atur koneksi database di `appsettings.json`.
3. Jalankan migrasi database.
4. Jalankan aplikasi dengan `dotnet run`.

## Endpoint API
- **GET** /todos - Ambil semua to-do
- **POST** /todos - Tambah to-do baru
- **PUT** /todos/{id} - Perbarui to-do
- **DELETE** /todos/{id} - Hapus to-do
