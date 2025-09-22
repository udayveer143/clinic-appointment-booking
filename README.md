# clinic-appointment-booking
Mini appointment booking system for a healthcare clinic

# Appointment Booking API

A mini **appointment booking system** for a healthcare clinic built with **.NET 8 Web API** and **in-memory database**.  
This API allows you to **book, update, list, and cancel appointments** with basic validation and business rules.

---

## Features

- CRUD operations for appointments:
  - `GET /appointments` – List all appointments
  - `GET /appointments/{id}` – Get appointment by ID
  - `POST /appointments` – Book a new appointment
  - `PUT /appointments/{id}` – Update an existing appointment
  - `DELETE /appointments/{id}` – Cancel appointment
- Validation:
  - Required fields (`PatientName`, `DoctorName`, `StartTime`, `EndTime`)
  - Name length ? 100 characters
  - `StartTime` must be now or in the future
  - `EndTime` must be after `StartTime`
  - No overlapping appointments for the same doctor
- Swagger/OpenAPI documentation
- CORS enabled for Angular frontend (`http://localhost:4200`)
- Business logic separated in **service layer** (thin controller)

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code
- Optional: [Docker](https://www.docker.com/) for containerization

---

## Setup Instructions

### 1. Clone the repository

```bash
git clone https://github.com/your-username/appointment-api.git
cd appointment-api
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Run the API

```bash
dotnet run
```

- By default, the API will run on `https://localhost:5001` and `http://localhost:5000`
- Swagger UI available at `https://localhost:5001/swagger/index.html`

### 4. Test the API

- You can use **Swagger UI** or **Postman** to test endpoints.
- Example payload for booking an appointment:

```json
{
  "patientName": "Rahul Kumar",
  "doctorName": "Dr. Neha",
  "startTime": "2025-09-22T14:00:00",
  "endTime": "2025-09-22T15:00:00"
}
```

### 5. Optional: Docker

Build and run the API in Docker:

```bash
docker build -t appointment-api .
docker run -p 5000:5000 -p 5001:5001 appointment-api
```

---

## Project Structure

```
AppointmentAPI/
??? Controllers/       # API controllers
??? Models/            # Appointment model
??? Data/              # DbContext for in-memory database
??? Services/          # Business logic layer
??? Validation/        # Custom validation attributes
??? Program.cs         # Application entry point
??? AppointmentAPI.csproj
??? README.md
```

---

## Assumptions

- Only **future appointments** are allowed (`StartTime >= now`)
- Appointments **cannot overlap** for the same doctor
- Maximum length of patient and doctor names: 100 characters
- Using **In-Memory database** for simplicity; can be switched to SQLite or SQL Server if required
- CORS is configured for Angular frontend on `http://localhost:4200`

---

## Future Improvements

- Persist data to **SQLite or SQL Server**
- Authentication & Authorization (JWT)
- Notification emails for booked/cancelled appointments
- Frontend Angular application with forms and list view
- Unit and integration tests for services and controllers

---

## References

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core In-Memory Database](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
