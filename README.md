<p align="center">
  <img src="RideShare_Connect/wwwroot/Image/hero_carpool.png" alt="RideShare Connect" width="600"/>
</p>

<h1 align="center">RideShare Connect</h1>

<p align="center">
  <strong>A full-featured carpooling platform connecting drivers and passengers for cost-effective, eco-friendly, and trusted travel across India.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8.0"/>
  <img src="https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white" alt="MySQL 8.0"/>
  <img src="https://img.shields.io/badge/Docker-Ready-2496ED?style=for-the-badge&logo=docker&logoColor=white" alt="Docker"/>
  <img src="https://img.shields.io/badge/Razorpay-Payments-0C2451?style=for-the-badge&logo=razorpay&logoColor=white" alt="Razorpay"/>
  <img src="https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white" alt="Bootstrap 5"/>
</p>

---

## Features

### User Management & Authentication
- User & Driver registration with email verification (OTP via Brevo)
- Google & Facebook OAuth login
- Role-based access: **User**, **Driver**, **Admin**
- Password reset with secure token-based flow
- Profile management with avatar upload

### Ride Management & Booking
- Drivers can create, edit, and manage rides with route points
- Users can search and book available rides
- Real-time seat availability tracking
- Ride lifecycle management (Scheduled → In Progress → Completed)
- Recurring ride support

### Vehicle & Driver Management
- Vehicle registration with document upload (RC, Insurance, License)
- Admin-verified document approval workflow
- Maintenance record tracking
- Driver & User mutual rating system (5-star)
- Document expiry reminders via background service

### Payment & Financial Management
- **Razorpay** payment gateway (INR)
- In-app wallet system for both Users and Drivers
- 90/10 commission split (Driver/Platform)
- Refund processing
- Complete transaction history and financial dashboard

### Admin Panel
- Comprehensive dashboard with analytics
- User and Driver moderation
- Vehicle and document verification
- Revenue tracking and commission management
- Platform wallet and transaction oversight

---

## Quick Start with Docker

> **Run the entire application with just 3 commands — no SDK or database installation needed.**

### Prerequisites

Install **Docker Desktop** for your operating system:

| OS | Download Link |
|---|---|
| Windows | [Docker Desktop for Windows](https://docs.docker.com/desktop/setup/install/windows-install/) |
| macOS | [Docker Desktop for Mac](https://docs.docker.com/desktop/setup/install/mac-install/) |
| Linux | [Docker Engine for Linux](https://docs.docker.com/engine/install/) |

After installing, make sure Docker is **running** (you should see the Docker icon in your system tray/menu bar).

### Step 1 — Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/RideShare_Connect.git
cd RideShare_Connect
```

### Step 2 — Start the Application

```bash
docker-compose up --build
```

This single command will:
- Pull and start a **MySQL 8.0** database container
- Build the **.NET 8.0** application from source
- Run all **database migrations** automatically
- Start the web server on port **5062**

> First run takes 2-3 minutes to build. Subsequent runs start in seconds.

### Step 3 — Open in Browser

```
http://localhost:5062
```

That's it! The application is running.

---

### Managing the Application

**Stop the application:**
```bash
docker-compose down
```

**Stop and delete all data (fresh start):**
```bash
docker-compose down -v
```

**View application logs:**
```bash
docker-compose logs -f app
```

**Rebuild after code changes:**
```bash
docker-compose up --build
```

---

## Run Without Docker (Local Development)

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8.0](https://dev.mysql.com/downloads/mysql/)

### Setup

1. **Create the database:**
   ```sql
   CREATE DATABASE RideShareConnect;
   ```

2. **Update the connection string** in `RideShare_Connect/appsettings.json`:
   ```json
   "DefaultConnection": "Server=localhost;Port=3306;Database=RideShareConnect;Uid=root;Pwd=YOUR_PASSWORD;SslMode=None;"
   ```

3. **Run the application:**
   ```bash
   cd RideShare_Connect
   dotnet restore
   dotnet run
   ```

4. Open **http://localhost:5062** in your browser.

---

## Configuration

### Environment Variables (Docker)

Configure these in `docker-compose.yml` under the `app` service:

| Variable | Description |
|---|---|
| `ConnectionStrings__DefaultConnection` | MySQL connection string (pre-configured for Docker) |
| `Authentication__Google__ClientId` | Google OAuth Client ID |
| `Authentication__Google__ClientSecret` | Google OAuth Client Secret |
| `PaymentGateway__ApiKey` | Razorpay API Key |
| `PaymentGateway__SecretKey` | Razorpay Secret Key |

### Setting Up Google OAuth (Optional)

1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Create a new project or select an existing one
3. Navigate to **APIs & Services → Credentials**
4. Create an **OAuth 2.0 Client ID** (Web Application)
5. Add `http://localhost:5062/signin-google` as an authorized redirect URI
6. Copy the Client ID and Client Secret into `docker-compose.yml`

### Setting Up Razorpay Payments (Optional)

1. Create an account at [Razorpay Dashboard](https://dashboard.razorpay.com/)
2. Get your API Key and Secret from **Settings → API Keys**
3. Add them to `docker-compose.yml`

---

## Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | ASP.NET Core 8.0 MVC |
| **Database** | MySQL 8.0 (Pomelo EF Core) |
| **ORM** | Entity Framework Core 8.0 (Code-First) |
| **Authentication** | Cookie Auth + Google/Facebook OAuth |
| **Payments** | Razorpay (INR) |
| **Email** | Brevo (Sendinblue) SMTP |
| **Background Jobs** | Hangfire + Custom BackgroundService |
| **Frontend** | Bootstrap 5, jQuery |
| **Containerization** | Docker + Docker Compose |

---

## Project Structure

```
RideShare_Connect-cloud-main/
├── docker-compose.yml          # Docker orchestration
├── ER Diagrams/                # Database ER diagrams (PDF)
├── RideShare_Connect/
│   ├── Dockerfile              # Multi-stage build
│   ├── Program.cs              # Application entry point
│   ├── Controllers/            # MVC Controllers (14 controllers)
│   ├── Models/                 # Domain models (5 modules)
│   │   ├── AdminManagement/
│   │   ├── PaymentManagement/
│   │   ├── RideManagement/
│   │   ├── UserManagement/
│   │   └── VehicleManagement/
│   ├── Views/                  # Razor Views
│   ├── Services/               # Business logic & payment gateway
│   ├── DTOs/                   # Data Transfer Objects
│   ├── ViewModels/             # View-specific models
│   ├── Migrations/             # EF Core migrations
│   └── wwwroot/                # Static files (CSS, JS, Images)
└── UML Diagram/                # UML design diagrams
```

---

## Database Design

The application uses a **code-first** approach with Entity Framework Core. The database is organized into 5 modules:

1. **User Management** — Users, Profiles, Settings, Verification, Login History
2. **Ride Management** — Rides, Bookings, Route Points, Ride Requests
3. **Vehicle Management** — Vehicles, Drivers, Documents, Maintenance Records
4. **Payment Management** — Payments, Wallets, Transactions, Commissions, Refunds
5. **Admin Management** — Admins, Analytics, Audit Trails, System Logs

ER diagrams for each module are available in the [`ER Diagrams/`](ER%20Diagrams/) folder.

---

## License

This project is for educational purposes.
