# SSD Group 6 Deployment

An ASP.NET Core MVC web app (.NET 10) with ASP.NET Identity and role based access.

## Features

- **Companies**: view and manage companies
- **Products**: Employees and Supervisors can view, only Supervisors can create, edit and delete
- **Services**: Employees and Supervisors can view, only Supervisors can create, edit and delete
- **Message Board**: any logged in user can view and post messages (no edit or delete)

## Live Site

- URL: `LIVE_URL_HERE`
- Test account logins are provided in our submission document.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2026 (with the ASP.NET and web development workload)
- SQL Server LocalDB (installed with Visual Studio)

## Build and Run Locally

1. Clone the repo:
```
   git clone https://github.com/ellaa555/SSD-Group6Deployment.git
```
2. Open `SSD_Lab1EB.slnx` in Visual Studio.
3. Set the seed user passwords with User Secrets. Right click the `SSD_Lab1EB` project > **Manage User Secrets**, and paste:
```json
   {
     "Secrets": {
       "SupervisorPassword": "ChooseAPassword1!",
       "EmployeePassword": "ChooseAPassword2!"
     }
   }
```
   Passwords need an uppercase letter, a lowercase letter, a number and a symbol.
4. Press **F5** to run. The database is created and migrated automatically on startup.

## Seeded Accounts (local)

| Email | Role | Password |
|---|---|---|
| supervisor@lab1.com | Supervisor | Whatever you set in step 3 |
| employee@lab1.com | Employee | Whatever you set in step 3 |

You can also register a new account. After registering, click the confirmation link shown on the screen to activate it.