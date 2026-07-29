# VSA Demo

This repository is a .NET 10 Web API demo that shows a Vertical Slice Architecture (VSA) style application in a small, practical form. It uses MediatR for request handling, FluentValidation for input validation, and Dapper with SQLite for repository-backed data access.

## What the demo includes

- Two vertical slice features:
  - Container transfer via POST /container-transfer
  - Container unloading via POST /unload-container
- A simple in-memory integration event publisher and a SQLite-backed transfer repository for demo purposes
- Swagger/OpenAPI support for local exploration of the API
- Unit tests covering handlers and core behavior

## Architecture and project ownership

The solution is organized around the responsibilities reflected in the current implementation:

- src/VsaDemo.Api: the ASP.NET Core host project. It owns the application entry point, dependency injection registration, endpoint registration, and Swagger configuration.
- src/VsaDemo.Core: the VSA feature core project. It owns the feature slices and their handlers.
- src/VsaDemo.Infrastructure: the shared infrastructure project. It owns implementations such as the SQLite-backed repository, processing clients, and the in-memory event publisher.
- src/VsaDemo.Contracts: the shared contracts project. It owns request/response models, shared interfaces, and DTOs used across the application.

## Project structure

- src/VsaDemo.Api
- src/VsaDemo.Core
- src/VsaDemo.Infrastructure
- src/VsaDemo.Contracts
- tests/VsaDemo.Tests

## Prerequisites

- .NET 10 SDK
- A terminal or IDE with .NET support such as Visual Studio or VS Code

## Getting started

From the solution root, run:

```bash
dotnet restore
dotnet build
dotnet run --project src/VsaDemo.Api
```

The API will start with Swagger available in development mode. You can explore the endpoints and try the sample requests from the browser or from tools such as curl or Postman.

## Example requests

Container transfer:

```bash
curl -X POST "https://localhost:5001/container-transfer" \
  -H "Content-Type: application/json" \
  -d '{"containerId":"C-100","sourceLocation":"Dock-A","destinationLocation":"Bay-1"}'
```

Unload container:

```bash
curl -X POST "https://localhost:5001/unload-container" \
  -H "Content-Type: application/json" \
  -d '{"containerId":"C-100","wasteItems":[{"wasteType":"lubricants","quantityKg":12.5}]}'
```

## Running tests

```bash
dotnet test
```

## Notes

This repository is intended as a learning and demonstration project for Vertical Slice Architecture rather than a production-ready template.
