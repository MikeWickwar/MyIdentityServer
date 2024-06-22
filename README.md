# Duende IdentityServer with PostgreSQL

This project sets up a Duende IdentityServer integrated with ASP.NET Core Identity and PostgreSQL. It provides a secure and extensible foundation for handling user authentication and authorization in your applications.

## Table of Contents
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Database Management](#database-management)
- [License](#license)

## Prerequisites
- .NET SDK 6.0 or later
- PostgreSQL 12 or later
- Node.js (for running frontend applications, if applicable)

## Installation

### Clone the repository:

```sh
git clone https://github.com/your-username/duende-ids-postgresql.git
cd duende-ids-postgresql
```

### Install dependencies:

```sh
dotnet restore
```

### Install PostgreSQL:

Ensure PostgreSQL is installed and running. You can install it using Homebrew:

```sh
brew install postgresql
brew services start postgresql
```

### Create the PostgreSQL database:

```sh
psql -U postgres
CREATE DATABASE mydatabase;
CREATE USER myuser WITH ENCRYPTED PASSWORD 'mypassword';
GRANT ALL PRIVILEGES ON DATABASE mydatabase TO myuser;
\q
```

## Configuration

### Update the connection string:

Modify the `appsettings.json` file with your PostgreSQL connection details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=mydatabase;Username=myuser;Password=mypassword"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Ensure Proper Packages:

Make sure you have the necessary packages installed:

```sh
dotnet add package Duende.IdentityServer
dotnet add package Duende.IdentityServer.AspNetIdentity
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

## Running the Application

### Apply Migrations:

Create and apply the database migrations:

```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Run the application:

Start the application:

```sh
dotnet run
```

### Access the application:

Open your browser and navigate to [https://localhost:5001](https://localhost:5001).

## Database Management

### Listing Tables:

To list all tables in your PostgreSQL database:

```sql
SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';
```

### Querying Tables:

To select all records from a specific table (e.g., `AspNetRoleClaims`):

```sql
SELECT * FROM "AspNetRoleClaims";
```

## License

This project is licensed under the MIT License. See the LICENSE file for details.
