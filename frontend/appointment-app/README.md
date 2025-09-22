# Angular Appointment Booking App

## Overview
This is a **frontend Angular application** for a healthcare clinic appointment booking system.  
It interacts with a .NET Core Web API backend to **book, update, delete, and view appointments**.  

The app includes:

- Form to book or edit appointments  
- List of all appointments with search and filter  
- Client-side and server-side validation  
- Bootstrap styling and responsive layout  
- Display of success and error messages via Bootstrap alerts  
- Date/time pickers for better UX  

---

## Features

1. **Book Appointment**: Fill form with patient name, doctor name, start/end date-time.  
2. **Edit Appointment**: Update existing appointments.  
3. **Delete Appointment**: Cancel appointments directly from the list.  
4. **Search / Filter**: By patient name, doctor name, start date, end date.  
5. **Validation**:
   - Required fields  
   - Max length for text fields  
   - Start time must be in the future  
   - End time must be after start time  
   - Server-side validation errors shown in a single string alert  

---

## Technologies Used

- **Angular 20.3.2**  
- **Node.js 22.19.0**  
- **Bootstrap 5**  
- **Reactive Forms**  
- **Angular Router & Standalone Components**  
- **HTTP Client** for API interaction  

---

## Project Setup

### 1. Clone Repository

```bash
git clone <your-angular-repo-url>
cd <your-angular-project-folder>
```

### 2. Install Dependencies

```bash
npm install
```

### 3. Serve the Application Locally

```bash
ng serve
```

The app will run on `http://localhost:4200` by default.

### 4. Configuration

- Ensure the **backend API** is running and CORS is allowed (`http://localhost:4200`)  
- Update `environment.ts` if the API URL is different.

---

## Folder Structure

```
src/
  app/
    appointments/
      appointment-form/       # Form component for booking/editing
      appointment-list/       # List and filter component
    models/                   # Appointment interface
    services/                 # API service
```

---

## Usage

1. Navigate to `http://localhost:4200/appointments`.  
2. Use the **filters** at the top to search by doctor, patient, or date range.  
3. Click **"Add Appointment"** to open the form.  
4. Fill all required fields and click **Save**.  
5. Edit or Delete existing appointments using the corresponding buttons in the list.  
6. All validation errors (client-side or server-side) will appear as **Bootstrap alerts**.  

---

## Bootstrap / Styling Notes

- Forms and filters are styled using **Bootstrap grid system**.  
- Alerts show success (`alert-success`) or error (`alert-danger`) messages.  
- Inputs use **`datetime-local`** for start/end times for better UX.  

---

## Git & Deployment

- Ensure commits are **descriptive and modular**.  
- Example commit: `feat: add appointment form with validation`.  
- For deployment, you can build production-ready code:

```bash
ng build --prod
```

- Copy the `dist/` folder contents to your web server.  

---

## Assumptions

- Backend API is already running and accessible.  
- No authentication/authorization implemented yet.  
- Using Bootstrap for styling; no advanced UI framework.

