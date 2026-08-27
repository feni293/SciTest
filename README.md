# Senior Developer Technical Test

## Overview

This project implements a Product management API using ASP.NET Core and SQL Server.

The solution includes:

* Product CRUD operations
* SQL Server Stored Procedures for all database operations
* Input validation
* Business logic separated into the Domain layer
* Query and Command Handlers
* External API integration using Open-Meteo
* Swagger/OpenAPI documentation
* Unit tests

## Architecture

The solution follows a layered architecture with clear separation of responsibilities.

```text
SCITest.Api
      │
      ▼
SCITest.Application
      │
      ▼
SCITest.Domain
      ▲
      │
SCITest.Infrastructure
```

### Api

Responsible for HTTP concerns, controllers, middleware and API configuration.

### Application

Responsible for:

* DTOs
* Request validation
* Commands
* Queries
* Handlers
* Application-level orchestration

### Domain

Responsible for:

* Domain entities
* Business rules
* Domain services
* Repository abstractions

### Infrastructure

Responsible for:

* SQL Server connectivity
* Stored Procedure execution
* Repository implementations
* External API integrations

## Technologies

* .NET 8
* ASP.NET Core Web API
* SQL Server
* Microsoft.Data.SqlClient
* FluentValidation
* Swagger / OpenAPI
* xUnit
* Moq
* Open-Meteo API

## Prerequisites

Make sure the following are installed:

* .NET 8 SDK
* SQL Server
* Visual Studio 2022 or another compatible .NET IDE

## Database Setup

The database scripts are located in:

```text
/database
```

Execute the scripts in the following order:

```text
01_CreateDatabase.sql
02_CreateTables.sql
03_StoredProcedures.sql
```

The scripts create:

* `SeniorDeveloperTechnicalTestDb`
* `Products` table
* Product CRUD Stored Procedures

All Product database operations are executed through Stored Procedures.

## Configuration

Update the connection string in:

```text
SCITest.Api/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SeniorDeveloperTechnicalTestDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Adjust the SQL Server instance according to the local environment.

## Running the Application

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the API:

```bash
dotnet run --project SCITest.Api
```

Swagger will be available at:

```text
https://localhost:<port>/swagger
```

The port depends on the local launch configuration.

## Product API

### Create Product

```http
POST /api/Products
```

Example request:

```json
{
  "name": "Gaming Laptop",
  "description": "Laptop for development and gaming",
  "price": 4500000
}
```

### Get All Products

```http
GET /api/Products
```

### Get Product

```http
GET /api/Products/{id}
```

### Update Product

```http
PUT /api/Products/{id}
```

Example:

```json
{
  "name": "Gaming Laptop Updated",
  "description": "Updated product description",
  "price": 4800000
}
```

### Delete Product

```http
DELETE /api/Products/{id}
```

## External API Integration

The application integrates with Open-Meteo to retrieve current weather information for a city.

Endpoint:

```http
GET /api/Weather/{city}
```

Example:

```http
GET /api/Weather/Bogota
```

The application first resolves the city coordinates using the Open-Meteo Geocoding API and then retrieves the current weather using the Forecast API.

The external API response is mapped to an application-specific DTO rather than being exposed directly.

## Testing

Run all tests with:

```bash
dotnet test
```

The test project contains unit tests for Product business logic using xUnit and Moq.

## Project Structure

```text
SCITest
│
├── SCITest.Api
│   ├── Controllers
│   └── Middleware
│
├── SCITest.Application
│   ├── DTOs
│   ├── Handlers
│   ├── Interfaces
│   └── Validators
│
├── SCITest.Domain
│   ├── Entities
│   ├── Interfaces
│   └── Services
│
├── SCITest.Infrastructure
│   ├── Data
│   ├── Repositories
│   └── ExternalServices
│
├── SCITest.Tests
│   └── Services
│
├── database
│   ├── 01_CreateDatabase.sql
│   ├── 02_CreateTables.sql
│   └── 03_StoredProcedures.sql
│
└── README.md
```

