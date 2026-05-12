# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

### Docker (full stack)
```bash
docker-compose up --build       # Start all services (SQL Server, WebAPI, React)
docker-compose down             # Stop all services
```

### Frontend (`FullStackDemo.Frontend/`)
```bash
npm run dev       # Dev server with host enabled (port 5173)
npm run build     # TypeScript check + Vite production build
npm run lint      # ESLint
npm run preview   # Preview production build
```

### Backend (`FullStackDemo.WebApi/`)
```bash
dotnet run                          # Run API (port 5028)
dotnet build FullStackDemoSolution.sln   # Build entire solution
```

## Architecture

### Backend — Layered DDD + CQRS

Four C# projects with strict dependency flow:

```
WebApi → ApplicationService → Domain ← Infrastructure
                                  ↑
                               Common
```

- **Domain**: Entities + repository interfaces. No external dependencies.
- **ApplicationService**: Commands (writes via EF Core) and Queries (reads via Dapper), DTOs, handlers.
- **Infrastructure**: EF Core `GundamDbContext` + Dapper `GundamDb` (query interface), repository implementations, DB seeder.
- **WebApi**: Controllers, filters, AutoMapper profiles, DI wiring in `Configurations/`.

DI is split across dedicated files in `FullStackDemo.WebApi/Configurations/` — `AuthenticationConfiguration`, `DbContextConfiguration`, `RepositoryConfiguration`, `ServiceConfiguration`, `LoggingConfiguration`.

### Authentication

Two schemes coexist:
- **Basic Auth**: enforced via `BasicAuthFilter` attribute on protected endpoints; credentials stored in DB and compared via `HashingHelper`.
- **JWT Bearer**: issued by `SecurityController`, used by the React frontend for subsequent API calls.

### Database

- **Local dev**: SQL Server 2022 via Docker Compose (mapped to port `14330`).
- **Production**: SQL Server; connection string `GundamDb` in `appsettings.Production.json` (points to `sqlserver` Docker service on Render).
- Both EF Core and Dapper are wired to the same database; EF Core handles writes, Dapper handles reads.
- `DbSeeder.cs` seeds initial data on startup.

### Frontend — React 18 + TypeScript + Vite 5

Stack: **Vite 5** (pinned — Node 20.17 can't run Vite 8+), **shadcn/ui** components, **Tailwind CSS v4** via `@tailwindcss/vite` plugin (no `tailwind.config.js` — theme vars in `index.css`).

Key conventions:
- **Path aliases** (`@/`, `@components`, `@services`, `@types`, `@utils`, `@context`, `@enums`, `@pages`) defined in `vite.config.ts` and both `tsconfig.json` + `tsconfig.app.json`. Use `@/types/` not `@types/` (conflicts with TypeScript's built-in namespace).
- **Axios instances** in `src/utils/` — `AxiosBearer.ts` (JWT) and `AxiosBasicAuth.ts` (basic auth) — used by services.
- **Forms**: React Hook Form + Zod resolvers; schema defined inline per page/component.
- **API constants**: `ApiEndPoints` in `src/enums/ApiConstants.ts` is a `const` object (not `enum` — `erasableSyntaxOnly` blocks enums).
- **API response shape**: `IApiBodyResponse` has top-level `data`, `totalCount`, `success` — not nested.
- **API base URL** comes from `VITE_API_URL` in `.env`. Auth credentials from `VITE_BASIC_AUTH_API_UID` / `VITE_BASIC_AUTH_API_PWD`.

### Docker Compose Services

| Service | Image | Host Port |
|---|---|---|
| `sqlserver` | SQL Server 2022 | 14330 |
| `webapi` | ASP.NET Core 8 | 5028 |
| `react` | Vite preview | 5173 |

All services share the `app-network` bridge. The WebAPI Dockerfile is at the solution root; the React Dockerfile is inside `FullStackDemo.Frontend/`.