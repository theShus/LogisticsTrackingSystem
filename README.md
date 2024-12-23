# Logistics Tracking System

A modern web application for tracking shipments and managing logistics operations.

## Project Structure

- **LogisticsTrackingSystem.Api**: Backend REST API built with .NET 8.0
- **LogisticsTrackingSystem.Presentation**: Frontend Blazor WebAssembly application

## Features

- Shipment tracking and management
- User authentication and authorization
- Real-time shipment status updates
- Search and filter shipments
- Secure API endpoints with JWT authentication
- Modern and responsive UI

## Prerequisites

- .NET 8.0 SDK
- A modern web browser
- Visual Studio 2022 or VS Code (recommended)

## Getting Started

1. Clone the repository
2. Navigate to the project directory

### Running the API (Backend)

1. Navigate to the API project:
```bash
cd LogisticsTrackingSystem.Api
```

2. Run the API:
```bash
dotnet run
```

The API will be available at:
- HTTPS: https://localhost:7076
- HTTP: http://localhost:5007
- Swagger UI: https://localhost:7076/swagger

### Running the Frontend

1. Navigate to the Presentation project:
```bash
cd LogisticsTrackingSystem.Presentation
```

2. Run the Blazor application:
```bash
dotnet run
```

## API Endpoints

### Authentication
- POST `/api/auth/login`: Authenticate user and get JWT token
  - Body: `{ "username": "string", "password": "string" }`

### Shipments
- GET `/api/shipments`: Get all shipments
- GET `/api/shipments/{id}`: Get shipment by ID
- POST `/api/shipments`: Create new shipment (requires authentication)
- PUT `/api/shipments/{id}`: Update shipment (requires authentication)
- DELETE `/api/shipments/{id}`: Delete shipment (requires Admin role)

## Authentication

The system uses JWT (JSON Web Tokens) for authentication. Two default users are seeded:

1. Admin User - has all permissions 
   - Username: admin
   - Password: admin123
   - Role: Admin

2. Regular User - has edit permission
   - Username: user
   - Password: user123
   - Role: User
  
3. No user - has view permissions

## Database

The application uses Entity Framework Core with an in-memory database for demonstration purposes. The database is seeded with sample shipments and users on startup.

## Shipment Statuses

Shipments can have one of the following statuses:
- InTransit
- Delivered
- InWarehouse

## Security

- API endpoints are secured with JWT authentication
- Role-based authorization (Admin and User roles)
- CORS is configured to allow frontend access
- Secure communication over HTTPS

## Development Notes

- The API uses an in-memory database for simplicity. For production, configure a proper database system.
- JWT secret key should be properly secured in production environment
- Password hashing should be implemented for production use
- CORS settings should be restricted to specific origins in production
