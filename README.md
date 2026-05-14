# Fullstack Demo Project

A full-stack demo application built with **ASP.NET Core 8** (backend) and **React 18** (frontend), containerized via Docker Compose.

## Overview

The Fullstack Demo Project follows the Domain-Driven Design (DDD) pattern with a CQRS approach. It utilizes SQL Server for database management, Dapper for querying, and Entity Framework Core (EF Core) for command handling. The frontend is built with React 18 and TypeScript, served via Vite 5.

## Technologies Used

### Backend
- **ASP.NET Core 8** — web framework for building backend services
- **SQL Server 2022** — database management system
- **Dapper** — micro ORM for efficient data access and querying
- **Entity Framework Core** — ORM for command handling using CQRS
- **JWT Bearer + Basic Auth** — dual authentication schemes
- **Visual Studio** — IDE for development and publishing
- **SQL Server Data Tools (SSDT)** — tools for developing, building, testing, and publishing SQL Server databases

### Frontend
- **React 18 + TypeScript**
- **Vite 5** — build tool
- **shadcn/ui** — component library
- **Tailwind CSS v4** — styling
- **React Hook Form + Zod** — forms and validation
- **Axios** — HTTP client

> Frontend was built with the assistance of **[Claude Code](https://claude.ai/code)** (Anthropic AI) using the following installed skill agents:
> - **shadcn** — component scaffolding, composition, and styling
> - **tailwind-v4-shadcn** — Tailwind CSS v4 + shadcn/ui setup, theming, and dark mode patterns
> - **vercel-react-best-practices** — React performance optimization guidelines from Vercel Engineering

## Features

- **Domain-Driven Design (DDD):** Organized codebase into domains for better maintainability and scalability.
- **Database Management:** Integrated with SQL Server for robust data storage.
- **Data Access:** Utilizes Dapper for efficient querying and EF Core for commands.
- **CQRS:** Implements CQRS pattern for separating read and write operations.
- **Authentication:** Basic Auth for protected endpoints; JWT Bearer for frontend API calls.
- **Dockerized:** Full stack runs via a single `docker-compose up --build` command.

## Architecture

### Backend — 4-project layered DDD/CQRS

```
WebApi → ApplicationService → Domain ← Infrastructure
                                  ↑
                               Common
```

- **Domain** — entities and repository interfaces
- **ApplicationService** — commands (EF Core) and queries (Dapper), DTOs, handlers
- **Infrastructure** — `GundamDbContext`, Dapper query interface, repository implementations, DB seeder
- **WebApi** — controllers, filters, AutoMapper profiles, DI configuration

## Getting Started

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(for containerized setup)*
- .NET 8 SDK
- Node.js 20.x
- SQL Server + SQL Server Management Studio (SSMS) *(local dev only)*

### Run with Docker Compose (recommended)

```bash
docker-compose up --build
```

| Service | URL |
|---|---|
| React frontend | http://localhost:5173 |
| WebAPI | http://localhost:5028 |
| SQL Server | localhost:14330 |

Stop all services:
```bash
docker-compose down
```

### Run locally without Docker

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/FullStackDemo.git
   ```

2. Publish the SQL project using Visual Studio:
   - Right-click on the SQL project (`FullStackDemo.SQL.GundamDb`) in Solution Explorer.
   - Select **Publish**.
   - Configure the target database connection in the Publish Database dialog.
   - Click **Publish** to deploy the database.

3. Update the SQL connection string in `FullStackDemo.WebApi/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "GundamDb": "Server=your_server_name;Database=GundamDb;User Id=your_username;Password=your_password;TrustServerCertificate=True;"
   }
   ```

4. Run the backend:
   ```bash
   dotnet run --project FullStackDemo.WebApi
   ```

5. Set up and run the frontend:
   ```bash
   cd FullStackDemo.Frontend
   npm install
   npm run dev
   ```

### Environment Variables (Frontend)

Create `FullStackDemo.Frontend/.env`:
```ini
VITE_API_URL=http://localhost:5028
VITE_BASIC_AUTH_API_UID=your_username
VITE_BASIC_AUTH_API_PWD=your_password
```

## API Documentation

After running the application, Swagger UI is available at:
```
http://localhost:5028/swagger
```

This interface provides a comprehensive overview of the available API endpoints, including request/response models and interactive testing capabilities.
