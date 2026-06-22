# GameHub

GameHub is a platform for browsing and managing video games — think Steam, but (soon to be) better.

Today you can browse the game catalog, filter by platform and studio, and edit game details. There's plenty more on the way.

## Getting Started

Everything runs in Docker, so you don't need .NET, Node, or SQL Server installed locally — just Docker.


   ```bash
   docker compose up --build
   ```

That's it. On first run the API applies database migrations and seeds sample data automatically.

| Service  | URL                                |
| -------- | ---------------------------------- |
| Frontend | http://localhost:4200              |
| API      | http://localhost:8080              |
| API docs | http://localhost:8080/scalar/v1    |
| Database | localhost:1433 (SQL Server)        |

To stop the stack, press `Ctrl+C`, or run `docker compose down`. Add `-v` to also wipe the database volume.

## Database Migrations & Seeding

The schema is managed with **EF Core migrations**, and a small set of sample data is **seeded** on startup. In development (the default for `docker compose up`) both run automatically:

- **Migrations** — the API applies any pending migrations on boot, creating/updating the schema. This is idempotent: an already-current database is left untouched.
- **Seeding** — after migrating, the API seeds sample studios, platforms, and games. The seeder is skipped if any games already exist, so it never duplicates data and is safe to run on every start.

## Tech Stack

### Backend

- **.NET 10** / **ASP.NET Core** Web API - modular monolith
- **Domain-Driven Design** — aggregates, entities, and value objects with smart constructors
- **CQRS** with **MediatR** —  queries and commands flow through SRP dedicated handlers
- **Clean architecture** — separate Domain, Application, and Persistence (Infrastructure) layers
- **Entity Framework Core 10** with **SQL Server 2022**
  - Value-object conversions, owned collections, and indexed lookups for platform/studio filtering
  - Shadow properties for `CreatedAt` / `ModifiedAt` auditing — kept out of the domain model
  - Automatic migrations and data seeding on startup (development)
- **Scalar** — interactive API reference over the built-in OpenAPI document

### Frontend

- **Angular 22** — standalone components, signals, and the modern control-flow template syntax
- **Angular Router** — separate browse and edit pages
- **ng-bootstrap** + **Bootstrap 5** — UI components and styling
- A dev-server proxy so the SPA talks to the API with no CORS fuss

### Infrastructure

- **Docker Compose** orchestrates the database, API, and frontend
- Hot-reload frontend container and a health-gated API that waits for the database to be ready

## Project Structure

```
GameHub/
├── docker-compose.yml
├── src/
│   ├── backend/          # .NET 
│   └── frontend/         # Angular
```
