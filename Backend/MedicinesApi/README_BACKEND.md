# Backend - MedicinesApi (.NET 8)
## Quick start
Requirements: .NET 8 SDK

1. Open terminal:
   cd backend/MedicinesApi
2. Restore packages:
   dotnet restore
3. Run:
   dotnet run --urls "http://localhost:5000"

The API will be available at http://localhost:5000
- GET /api/medicines
- POST /api/medicines
- PUT /api/medicines/{id}
- DELETE /api/medicines/{id}

Note: Authentication with Google is scaffolded but requires you to set ClientId/ClientSecret in appsettings.json or environment variables.
