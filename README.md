# 🧠 AI Chatbot – Real-Time Chat with Tavily AI

A production-ready chatbot platform built with **ASP.NET Core 8**, **SignalR**, **Entity Framework Core (Code-First)**, and **Tavily AI** for real-time AI-powered conversations.

---

## 🚀 Features

- ✅ **Real-Time AI Chat** using SignalR
- 🗂 **Session-Based Chat History**
- ✏️ **Edit, Delete, Approve Messages**
- 🔐 **JWT Authentication & Authorization**
- 🔄 **Infinite Scroll for Past Messages**
- 🧠 **Powered by Tavily AI**

---

## 📁 Project Structure

```
AI_Chatbot/
│
├── Controllers/           # API Controllers (Auth, Chat)
├── DTOs/                  # Data Transfer Objects
├── Interfaces/            # Service Interfaces
├── Models/                # Entity Models & DbContext
├── Services/              # Business Logic Implementation
├── Migrations/            # EF Core Migrations
├── appsettings*.json      # App configuration
└── Program.cs             # Main Entry Point
```

---

## 🔧 Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core (Code-First)**
- **SignalR**
- **JWT Authentication**
- **SQL Server**
- **Tavily AI API**

---

## 🛠️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Fahim2280/ai-chatbot.git
cd ai-chatbot
```

### 2. Configure Database & Secrets

Update the `appsettings.json` or `appsettings.Development.json` with your connection string and Tavily API key:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AIChatbotDb;Trusted_Connection=True;"
},
"Tavily": {
  "ApiKey": "YOUR_TAVILY_API_KEY"
}
```

### 3. Apply Migrations

```bash
dotnet ef database update
```

### 4. Run the Project

```bash
dotnet run
```

Visit: [https://localhost:5001/swagger](https://localhost:5001/swagger) for API documentation.

---

## 🔐 Authentication

Endpoints are secured using **JWT Bearer tokens**. Register or log in to receive a token.

Example:

```bash
POST /api/auth/register
POST /api/auth/login
Authorization: Bearer <token>
```

> 📝 **Note:** After logging in and receiving your JWT token, make sure to **authorize the token** before accessing any protected API endpoints.  
> You can do this by setting the `Authorization` header manually:

```http
Authorization: Bearer <your_token_here>
```

Or, in **Swagger UI**, click the **"Authorize"** button at the top, paste your token, and hit "Authorize" to test secured endpoints easily.

---

## 📡 SignalR Integration (Planned)

Real-time communication support using **SignalR** will enable live chat and instant updates.

---

## 📬 API Endpoints Overview

### 🔑 Auth (`/api/auth`)

- `POST /register` – Register a new user
- `POST /login` – Authenticate user
- `POST /logout` – Logout session

### 💬 Chat (`/api/chat`)

- `POST /send` – Send a chat message
- `GET /history?sessionId=...` – Get session chat history
- `GET /responses` – Get all AI responses
- `PUT /{id}` – Edit message
- `DELETE /{id}` – Soft delete message
- `PATCH /{id}/approve` – Approve a message

---

## 🧪 Testing

You can use **Postman**, **Swagger**, or **curl** to test the endpoints.

---

## 🤖 Powered by Tavily AI

For more information on Tavily AI, visit: [https://www.tavily.com](https://www.tavily.com)

---

## 📌 Future Enhancements

- 🔔 Live updates with **SignalR**
- 📱 Frontend using **Blazor**, **React**, or **Vue**
- 📊 Admin panel for analytics & moderation
- 🌐 Deploy to **Azure**, **AWS**, or **Docker**

---

## 📄 License

MIT License © 2025 Md Fahim Alam

---

## 🗃️ Database Setup with EF Core

### Create a Migration

```bash
dotnet ef migrations add <MigrationName>
```

### Apply the Migration

```bash
dotnet ef database update
```

> Replace `<MigrationName>` with an appropriate name for your changes.
