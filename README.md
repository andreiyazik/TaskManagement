
# Task Management Application

This project consists of two parts:
1. **Backend (ASP.NET Core)** - A RESTful API for managing tasks built on .NET 9.
2. **Frontend (Angular)** - A web application for interacting with the API built on Angular 19.

---

## Backend (ASP.NET Core)

### Features
- CRUD operations for tasks.
- Exception handling middleware for standardized error responses.
- CORS enabled for communication with the Angular frontend.

### API Endpoints

| Method | Endpoint             | Description                       |
|--------|----------------------|-----------------------------------|
| POST   | `/Tasks`             | Creates a new task.              |
| GET    | `/Tasks`             | Retrieves a paginated list of tasks. |
| PUT    | `/Tasks/{id}/status` | Updates the status of a task.    |

### Error Handling
The API includes a middleware to catch unhandled exceptions and return a consistent error response:
```json
{
  "Message": "An error occurred while processing your request.",
  "Details": "Detailed error message (in development mode)."
}
```

### Setting Up the Backend
1. Clone the repository.
2. Navigate to the backend project directory.
3. Install dependencies:
   ```bash
   dotnet restore
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. By default, the API runs at `https://localhost:7077`.

---

## Frontend (Angular)

### Features
- Task list with pagination.
- Task creation form with validation.
- Centralized navigation between the task list and task creation form.
- Exception handling for failed API calls.

### Structure

```
src/
├── app/
│   ├── app.component.ts
│   ├── components/
│   │   ├── task-list/
│   │   ├── task-form/
│   │   ├── task-management/
│   ├── models/
│   ├── services/
├── environments/
```

### Task List
- Displays tasks in a paginated table.
- Includes actions to update task statuses.

### Task Form
- Form for creating new tasks.
- Includes "Create Task" and "Cancel" buttons.
- Navigates back to the task list on successful creation or cancelation.

### Setting Up the Frontend
1. Navigate to the frontend project directory.
2. Install dependencies:
   ```bash
   npm install
   ```
3. Run the development server:
   ```bash
   ng serve
   ```
4. Open your browser and navigate to `http://localhost:4200`.

### Backend Integration
The Angular application fetches the backend base URL from environment files.

#### Environment Configuration
**src/environments/environment.ts**
```typescript
export const environment = {
  production: false,
  backendBaseUrl: 'https://localhost:7077',
};
```
