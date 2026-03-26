# 🗺️ GIS Asset Collection System

## 📌 Overview

A GIS-based system for managing assets, tracking contracts, and handling payment collections.
The system enables administrators to monitor performance, visualize assets on a map, and track financial data in real time.

---

## 🚀 Features

### 🔹 Asset Management

* Create, update, and delete assets
* Assign assets to regions
* Link each asset with a contract (One-to-One)
* Display assets on interactive GIS map

### 🔹 Contract Management

* Manage tenant and contract data
* Track installments and payments

### 🔹 Payment Collection

* Record installment payments
* Handle late payments
* Prevent duplicate monthly payments
* Auto-calculate late amounts and remaining installments

### 🔹 GIS Map

* Show assets on map using Leaflet
* Filter by region
* Auto zoom & focus on selected area
* Popup with asset details

### 🔹 Dashboard

* Total assets
* Total late payments
* Daily collections
* Collector performance

---

## 🧠 Business Logic

### ✔ Installment Payment

* Decrease remaining installments by 1
* Mark month as paid

### ✔ Late Payment

* Deduct from late amount
* Update remaining late balance

### ✔ Combined Payment

* Update installments
* Deduct late amount
* Log payment

---

## 🏗️ Architecture

* ASP.NET Core MVC
* Entity Framework Core
* Repository Pattern
* Unit of Work Pattern
* SQL Server

---

## 🗂️ Database Design

* Asset ↔ Contract → One-to-One
* Asset ↔ Region → Many-to-One
* Payment ↔ Asset → Many-to-One
* Payment ↔ Collector → Many-to-One

---

## 🔐 Roles & Default Users (Auto Seeded)

The system automatically creates roles and default users at startup:

### Roles:

* Admin
* Collector
* NewUser

### Default Users:

| Role      | Email                                     | Password |
| --------- | ----------------------------------------- | -------- |
| Admin     | [admin@gmail.com](mailto:admin@gmail.com) | P@ssW0rd |
| Collector | [user1@gmail.com](mailto:user1@gmail.com) | P@ssW0rd |
| NewUser   | [user2@gmail.com](mailto:user2@gmail.com) | P@ssW0rd |

---

## ⚙️ Database Migration & Seeding

The application automatically applies migrations and seeds initial data on startup.

### ✔ What happens automatically:

* Apply all pending migrations
* Create roles (Admin, Collector, NewUser)
* Create default users
* Assign roles to users

### 🔧 Implementation:

```csharp
await dbContext.Database.MigrateAsync();
await SeedData.SeedAppAsync(userManager, roleManager, dbContext);
```

### ✔ Logging:

* Success log when migration completes
* Error handling with logging

---

## 🧪 Validation Rules

* Prevent duplicate contract assignment
* Prevent duplicate monthly payments
* Prevent overpayment of late fees
* Maintain data consistency on delete

---

## 📊 Dashboard Features

* KPI cards
* Collector performance table
* Interactive GIS map with filters

---

## 🛠️ Technologies

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* Leaflet.js
* Bootstrap
* jQuery / DataTables

---

## ⚙️ How to Run

1. Clone the repository
2. Configure database connection in `appsettings.json`
3. Run the project

### ✔ No manual setup required:

* Database will be created automatically
* Users and roles will be seeded automatically

---

## 👤 Roles

### Admin

* Full system control
* Dashboard & analytics access

### Collector

* Collect payments
* View assigned assets

---

## 📌 Notes

* Clean architecture applied
* Scalable design
* Real-world business logic implementation
* GIS integration for asset tracking

---

## 👨‍💻 Author

**Mohamed Basiouny**
