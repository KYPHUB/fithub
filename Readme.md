# 🏋️ FitHub — Gym Management System

A comprehensive **Gym Management and Appointment System**. Developed using ASP.NET Core MVC architecture, database management with Entity Framework Core, and user authorization provided by the Identity library.

## 🚀 Features

### 👤 Member Panel
- **Registration & Login:** Secure membership system.
- **Booking Appointments:** Dynamic appointment creation based on trainers' availability.
- **Appointment History:** View and cancel pending, approved, and past appointments.
- **Smart Assistant (AI):** Personalized workout/diet plan generation with Google Gemini AI.
- **Trainer & Service Review:** Detailed trainer profiles and service descriptions.

### 🛠 Admin Panel
- **Dashboard:** General statistics and quick access.
- **Gym & Service Management:** CRUD operations for gym details and service categories.
- **Trainer Management:** Add trainers, upload photos, and assign specialization areas.
- **Working Hours:** Weekly working schedule for trainers and conflict checking.
- **Appointment Approval:** Approve or reject appointment requests from members.

### 🔌 Technical Specifications
- **Architecture:** ASP.NET Core MVC (.NET 9.0)
- **Database:** MS SQL Server (Entity Framework Core Code-First)
- **Authorization:** ASP.NET Core Identity (Role-Based: Admin, Member)
- **API:** RESTful API endpoints (Documented with Swagger UI)
- **AI Integration:** Google Gemini API (optional — runs in mock mode without API key)
- **Security:** CSRF protection, file upload validation, XSS sanitization, account lockout
- **Localization:** Turkish (tr-TR) culture support

---

## ⚙️ Installation and Execution

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (LocalDB or Full) — Comes with Visual Studio

### 1. Clone the Project
```bash
git clone https://github.com/USERNAME/FitHub.git
cd FitHub
```

### 2. Create the Database
```bash
cd FitnessApp.Web
dotnet ef database update
```
> This command automatically creates the database and adds seed data (admin user, trainers, services, etc.).

### 3. (Optional) Define Gemini AI API Key

A Google Gemini API key is required for the Smart Assistant feature to work with **real AI**.  
Without an API key, the application works in **mock (demo) mode** and does not throw errors.

If you have an API key, run the following command:

```bash
cd FitnessApp.Web
dotnet user-secrets set "Gemini:ApiKey" "WRITE_YOUR_API_KEY_HERE"
```

> 💡 **What are User Secrets?** Your API key is stored safely on your computer and is **never** committed to Git.  
> To get an API key: [Google AI Studio](https://aistudio.google.com/apikey)

### 4. Run the Project
```bash
dotnet run
```
Navigate to the address shown in your browser (usually `https://localhost:PORT`).

> 📘 Swagger API documentation: Available at `/swagger` (only in Development mode).

---

## 🔑 Login Information

| Role | Email | Password |
|------|-------|----------|
| **Admin** | `admin@saufitness.com` | `Admin1234` |
| **Member** | You can create a new account from the registration page | — |

---

## 📚 API Endpoints

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| `GET` | `/api/Trainers` | List of all trainers | Auth required |
| `GET` | `/api/Trainers/filter?date=YYYY-MM-DD` | Available trainers by date | Auth required |
| `GET` | `/api/Appointments/my-history` | Member's appointment history | Auth required |

---

## 🤖 Smart Assistant (AI)

Members can get a personalized nutrition and workout plan by entering their age, weight, height, and goals via the **My Account → AI Assistant** menu.

- **If Gemini API key is defined:** Generates a real-time plan with Google Gemini AI.
- **If no API key:** The application automatically runs in demo/mock mode, generating a plan from ready templates.

---

## 📁 Project Structure

```
FitnessApp.Web/
├── Areas/Admin/          # Admin panel (Controller + View)
├── Controllers/          # Main controllers
│   └── Api/              # RESTful API controllers
├── Data/                 # Entity models, DbContext, Seeder
├── Migrations/           # EF Core migration files
├── Services/             # Business logic services (AI, Appointment)
├── ViewModels/           # Form and view models
├── Views/                # Razor View files
└── wwwroot/              # Static files (CSS, JS, images)
```