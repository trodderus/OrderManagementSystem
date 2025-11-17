# Order Management System

A lightweight **Order Management System API** built with **.NET 8**, following **Clean Architecture** and **CQRS** principles.
Handles products, orders, and provides basic reports.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQLite (optional, for inspecting the DB)
- Optional: [DB Browser for SQLite](https://sqlitebrowser.org/)

---

## Setup Instructions

1. **Clone the repository**

```bash
git clone https://github.com/trodderus/OrderManagementSystem.git
cd OrderManagementSystem
```

2. **Install the EF Core global tool**

```bash
dotnet tool install --global dotnet-ef --version 8.*
```

3. **Restore NuGet packages**

```bash
dotnet restore
```

---

## Database Setup (SQLite)

1. **Add Initial Migration**
```bash
dotnet ef migrations add InitialCreate \
  -p src/OrderManagementSystem.Infrastructure/OrderManagementSystem.Infrastructure.csproj \
  --startup-project src/OrderManagementSystem.Presentation/OrderManagementSystem.Presentation.csproj
```

2. **Apply Migration / Create Database**
```bash
dotnet ef database update \
  -p src/OrderManagementSystem.Infrastructure/OrderManagementSystem.Infrastructure.csproj \
  -s src/OrderManagementSystem.Presentation/OrderManagementSystem.Presentation.csproj
```

---

## Run the API

```bash
cd src/OrderManagementSystem.Presentation
dotnet run
```

You can then access the API by importing the Postman Collection: `OrderManagementSystem API.postman_collection.json`

Alternatively, you can run through Visual Studio and access the SwaggerUI on https://localhost:7285/swagger/index.html

---

