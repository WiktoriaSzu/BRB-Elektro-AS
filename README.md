## Table of contents

*  [General info](#general-information)
*  [Live Demo](#live-demo)
*  [Technologies](#technologies)
*  [Feautures](#features)
*  [Screenshots](#screenshots)
*  [Launch](#launch)


# Overview

A responsive website built for a real client, including a contact form with backend integration and email functionality.

Live Demo: https://brbelektro.no

---

## Technologies
* HTML
* CSS
* Bootstrap
* JavaScript
* C# (.NET)
* PostgreSQL


## Features

* Responsive design (desktop & mobile)
* Navigation between multiple pages (Home, Services, Contact)
* Contact form with backend integration
* Data stored in a PostgreSQL database
* Email notifications and automatic replies on form submission
* Reusable components (navbar & footer via JS injection)


## Screenshots

### Homepage
![Homepage](screenshots/homepage.png)

### Services Section
![Services](screenshots/services.png)

### Contact Form
![Contact](screenshots/contact.png)

### Form Submission
![Success](./screenshots/success.png)


## Requirements

Make sure you have the following installed:

* .NET SDK (version 6 or newer recommended)
* PostgreSQL (running locally or accessible remotely)
* Git (optional, for cloning the repository)


## Installation

### 1. Clone the repository

```
git clone <your-repo-url>
cd BRB-Elektro-AS/BRBelektro
```

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


### 3. Install dependencies

```
dotnet restore
```

## Running the application

Start the backend server:

```
dotnet run
```

You should see output like:

```
Now listening on: http://localhost:5212
```

## Access the app

Open your browser:

```
http://localhost:5212
```

## What we learned

* Connecting frontend forms to backend API
* Implementing email functionality using SMTP
* Structuring a full-stack project
* Building responsive layouts with CSS
* Creating reusable components using JS injection

## Future Improvements

* Add Entity Framework Core for structured database access
* Implement authentication
* Improve frontend structure (e.g., framework integration)

---

## License

This project is proprietary and was developed for BRB Elektro AS.  
All rights reserved. Unauthorized use, copying, or distribution of this code is not permitted.

