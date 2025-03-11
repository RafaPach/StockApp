# 📈 Stock App

This is a **.NET Core Web API** project that combines **stock market management** with user-driven interaction. Users can track their favorite stocks and equities, create custom portfolios, and participate in discussions via comments—all within a **secure**, **JWT-authenticated** user system.

## About This Project

This project was developed as part of my journey to deepen my knowledge of **C#** and **.NET Core**. It provides a hands-on learning experience where I'm exploring backend development concepts like building RESTful APIs, implementing **JWT authentication**, and applying clean architecture principles.

I challenged myself by creating a platform that integrates interactive elements and dynamic functionality, drawing inspiration from major live platforms.

---

## 🚀 Features

-  **JWT Authentication** (secure user authentication & authorization)
-  **User Registration & Login**
-  **Stock and Equities Management**
-  **Custom Portfolio Creation**
-  **Comments on Stocks & Discussions**
-  **Secure RESTful APIs (ASP.NET Core)**

---

## 🛠️ Tech Stack

- **Backend Framework**: ASP.NET Core Web API
- **Authentication**: JWT Bearer Tokens
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Dependency Injection**: Built-in ASP.NET Core DI
  
---

## ⚙️ Installation & Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/RafaPach/StockApp.git
   cd StockApp

2. **Update `appsettings.json`**

   Open the `appsettings.json` file and configure your database connection string and JWT settings. Example:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "your_connection_string_here"
   },
   "Jwt": {
     "Key": "your_jwt_secret_key"
   }


3. **Apply database migrations**

   Run the following command to create the database and apply any pending migrations:

   ```bash
   dotnet ef database update

4. **Run the API locally**

   Start the API by running:

   ```bash
   dotnet run

The API will be available at:
https://localhost:5001 or http://localhost:5000

You can also access the Swagger UI at:
https://localhost:5001/swagger
