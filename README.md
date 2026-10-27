# StockPilot — Inventory Management System

A student mini-project built with **ASP.NET Core MVC, C#, Entity Framework Core, and MySQL**. It demonstrates MVC architecture, database connectivity, CRUD, form handling, model validation, TempData state management, search/filtering, low-stock alerts, and a responsive UI.

## Requirements
- .NET 8 SDK
- MySQL Server (XAMPP is fine)
- Visual Studio 2022 or VS Code

## Setup (XAMPP / MySQL)
1. Install and start **MySQL** from XAMPP. Start Apache only if you need phpMyAdmin.
2. Open `http://localhost/phpmyadmin`.
3. Select the **SQL** tab and run:
   ```sql
   CREATE DATABASE inventory_management;
   ```
4. Open `appsettings.json`. The default connection is:
   `server=localhost;port=3306;database=inventory_management;user=root;password=;`
   If your MySQL root user has a password, enter it after `password=`.
5. Open a terminal in this project folder and run:
   ```bash
   dotnet restore
   dotnet run
   ```
6. Open the local URL printed in the terminal (usually `http://localhost:5xxx` or `https://localhost:7xxx`).

The app automatically creates the `Products` table the first time it connects, using EF Core `EnsureCreated()`. You do not need to run migrations for this starter project.

## Features
- Dashboard totals: product count, units in stock, estimated inventory value, low-stock count
- Add, view, edit, and delete products
- Required-field and numeric model validation
- Unique SKU validation (also enforced by a database unique index)
- Search by product name, SKU, or supplier
- Filter by category and low-stock status
- Low-stock alerts based on each product's reorder level
- Responsive Bootstrap interface
- Success messages using `TempData`

## Demo walkthrough
1. Open **Add Product** and add a product (example: Wireless Mouse, SKU ACC-001, category Accessories, price 599, quantity 4, reorder level 5).
2. Save it and open Inventory. Its low-stock badge should appear because quantity is at or below reorder level.
3. Edit the product and change quantity to 20.
4. Use search/category/stock filters.
5. Open Details, then test Delete using a demo record.

## MVC architecture
- **Model:** `Models/Product.cs` — product fields and validation rules
- **View:** `Views/` — Razor pages and responsive interface
- **Controller:** `Controllers/ProductsController.cs` — handles requests and CRUD actions
- **Database layer:** `Data/ApplicationDbContext.cs` — EF Core mapping to MySQL

## Important notes
- The project source is provided as a ready-to-run starter, but it could not be compiled in this environment because the .NET SDK is not installed here. Run `dotnet restore` and `dotnet run` on your computer to confirm package restore and local MySQL settings.
- Bootstrap and client-side validation libraries load from CDNs, so internet access is needed for those assets. Server-side validation remains enabled.
- Use a unique SKU for each product.
