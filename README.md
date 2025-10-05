# ECom-API (.NET Clean Architecture)

A structured **E-Commerce RESTful API** built with **ASP.NET Core** following the **Clean Architecture** pattern.  
The project provides backend services for products, users, authentication, orders, and more — ready to be connected with a frontend.

---

##  Table of Contents
- Structure
- Features
- Tech Stack
- Getting Started
- Contributing
- License

---
## Structure

- **Core** → Contains the domain models, interfaces, and business logic.  
- **Infrastructure** → Handles database operations, repositories, and Identity.  
- **API** → Exposes REST endpoints for the frontend or clients.

---

##  Features
-  **Product Management**
-  **Cart System**  
-  **Pagination** 
-  **RESTful API** 
-  **DTOs & AutoMapper**  
-  **Layered architecture** 

---

##  Tech Stack

- **.NET 8**
- **Entity Framework Core**
- **SQL Server (LocalDB)**
- **AutoMapper**
- **Swagger / Postman**

---

### **Prerequisites**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) or any IDE supporting .NET

---

## **Setup and Installation**

### **Option 1: Using the Provided RAR File**
- Download the project as a `.rar` file (provided separately).
- Extract the `.rar` file to your preferred directory.
- Open the project in Visual Studio (or your preferred IDE).

### **Option 2: Cloning from Repository (Link will be provided later)**
- The link to the repository will be shared once available.
---


### 1.Setup database connection string

In appsettings.json of the API project:

```
"ConnectionStrings": {
  "EcomDatabase": "Server=(localdb)\\ProjectModels;Database=FirstEcom;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
### 2.Apply migrations
Make sure EF Core tools are installed, then run:

```bash
cd Ecom.infrastructure
dotnet ef database update
```
### 3.Run the API
```bash
cd ../Ecom.API
dotnet run
```
