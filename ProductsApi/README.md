
# Products API

## Overview

A .NET 8 ASP.NET Core Web API that demonstrates
validated product CRUD operations, centralized
exception handling, standardized HTTP responses,
and pagination.

## Features

- Product CRUD endpoints
- Data Annotations validation
- Cross-property validation
- Unique SKU enforcement
- Global exception handling
- Problem Details error responses
- Pagination using data/meta envelope
- In-memory storage

## Requirements

- .NET 8 SDK
- Git
- Swagger UI for API testing

## How to Run

1. Clone this repository.
2. Open a terminal in the project directory.
3. Run `dotnet restore`.
4. Run `dotnet build`.
5. Run `dotnet run`.
6. Open the Swagger URL displayed in the terminal.

## API Endpoints

| Method | Endpoint | Success | Errors |
|---|---|---|---|
| GET | /api/products | 200 | 400, 500 |
| GET | /api/products/{id} | 200 | 404, 500 |
| POST | /api/products | 201 | 400, 409, 500 |
| PUT | /api/products/{id} | 200 | 400, 404, 409, 500 |
| DELETE | /api/products/{id} | 204 | 404, 500 |

## Pagination

The GET all endpoint accepts:
- `page` — page number, starting at 1
- `pageSize` — number of records per page, from 1 to 100

The response contains:
- `data` — the products for the requested page
- `meta` — page, pageSize, totalItems, and totalPages

## Error Handling

Errors use Problem Details responses.
Validation failures return 400, missing products
return 404, duplicate SKUs return 409, and
unexpected errors return 500.

Unexpected error responses do not expose stack traces.

## Storage

Products are stored in memory. Data is not
persisted after the application stops.

## Contributors

- Andrick — API implementation and integration
- Jacob — DTO validation and validation tests
