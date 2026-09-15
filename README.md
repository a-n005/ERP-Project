# ERP System

A personal ERP desktop application built with **C#**, **.NET Framework**, **Windows Forms**, **ADO.NET**, and **SQL Server**.

The project is developed as a practical application to improve my understanding of C#/.NET development, database programming, and layered application architecture.

## 🛠️ Technologies

- C#
- .NET Framework
- Windows Forms
- ADO.NET
- SQL Server
- T-SQL
- Stored Procedures
- Table-Valued Parameters (TVP)
- SQL Transactions
- Object-Oriented Programming (OOP)

## 🏗️ Architecture

The application follows a **Layered Architecture**:

```text
UI
 ↓
BLL
 ↓
DAL
 ↓
SQL Server
```

### Project Layers

- **UI** — Windows Forms interface and custom controls.
- **BLL** — Business logic and validation.
- **DAL** — Database access and SQL Server operations.
- **Core** — Shared components used across the application.

## 📦 Main Features

- Sales invoices
- Sales returns
- Purchase invoices
- Purchase returns
- Customer management
- Supplier management
- Product and inventory management
- Payment and receipt operations
- Stock quantity updates
- Customer balance adjustments

## 🔄 Database Operations

The project uses:

- SQL Server Stored Procedures
- Table-Valued Parameters (TVP)
- Database Transactions
- Foreign Key relationships
- Business-rule validation
- `SCOPE_IDENTITY()` for retrieving inserted IDs

Sales and return operations include validation for quantities, payments, stock availability, and related customer balances.

## 🧪 Testing

The project also contains a testing layer for database-related operations.

## 📚 Purpose

This is a personal learning project developed to gain practical experience in:

- C# and .NET development
- Windows Forms applications
- SQL Server and T-SQL
- ADO.NET
- Database design
- Layered Architecture
- Business logic and validation
- Database transactions

## 👨‍💻 Author

**Anas Abdullah**

Junior C# / .NET Developer
