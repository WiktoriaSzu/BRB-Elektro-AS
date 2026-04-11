# BRBelektro

A lightweight web application built with a static frontend and a modern ASP.NET Core backend.
The backend serves static files and exposes API endpoints connected to a PostgreSQL database.

---

## 🚀 Features

* Static frontend (HTML, CSS, JavaScript)
* Backend powered by .NET (ASP.NET Core)
* REST API endpoints
* PostgreSQL database integration

---

## 🧰 Requirements

Make sure you have the following installed:

* .NET SDK (version 6 or newer recommended)
* PostgreSQL (running locally or accessible remotely)
* Git (optional, for cloning the repository)

---

## 📦 Project Structure

```
BRB-Elektro-AS/
│
├── BRBelektro/
│   ├── wwwroot/        # Static frontend files
│   │   ├── index.html
│   │   ├── pages/
│   │   ├── partials/
│   │   └── static/
│   │
│   ├── Program.cs      # Backend entry point
│   ├── appsettings.json
│   └── BRBelektro.csproj
│
└── BRB-Elektro-AS.sln
```

---

## ⚙️ Setup

### 1. Clone the repository

```
git clone <your-repo-url>
cd BRB-Elektro-AS/BRBelektro
```

---

### 2. Configure the database

Edit `appsettings.json` and update the connection string:

```
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=your_db;Username=your_user;Password=your_password"
  }
}
```

Make sure:

* PostgreSQL is running
* The database exists
* Credentials are correct

---

### 3. Install dependencies

```
dotnet restore
```

---

## ▶️ Running the application

Start the backend server:

```
dotnet run
```

You should see output like:

```
Now listening on: http://localhost:5212
```

---

## 🌐 Access the app

Open your browser:

```
http://localhost:5212
```

---

## 🔌 API Endpoints

Example endpoint:

```
GET /api/test-db
```

Returns a test response from the PostgreSQL database.

---

## 🧪 Development Notes

* Static files are served from the `wwwroot` folder
* Backend routes are defined in `Program.cs`
* Frontend can call backend using `fetch('/api/...')`
* No CORS setup needed when running on the same origin

---

## 🚨 Troubleshooting

### 404 on `/`

* Ensure `index.html` is inside `wwwroot`

### Database connection errors

* Verify PostgreSQL is running
* Check connection string values

### Port already in use

* Stop other running instances or change port in `launchSettings.json`

---

## 📌 Future Improvements

* Add Entity Framework Core for structured database access
* Implement authentication
* Improve frontend structure (e.g., framework integration)

---

## 📝 License

This project is for educational/development purposes.
