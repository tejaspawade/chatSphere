# ChatSphere

**chatSphere** is a real-time chat application built using ASP.NET Core (C#) and SignalR. It allows users to connect, join chat rooms/hubs, and exchange messages instantly in a web interface.

## Table of Contents

- [Demo / Screenshot](#demo--screenshot)  
- [Features](#features)  
- [Tech Stack](#tech-stack)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation & Setup](#installation-&-setup)  
  - [Running the Application](#running-the-application)  
- [Project Structure](#project-structure)  

## Demo / Screenshot
-Login Page
<img width="1750" height="925" alt="Screenshot 2025-11-02 173428" src="https://github.com/user-attachments/assets/ae4ffc11-669c-4ef4-bf0a-55c389b7d7a8" />

-Register Page
<img width="1678" height="875" alt="Screenshot 2025-11-02 173439" src="https://github.com/user-attachments/assets/37de19c5-a07e-4884-93f4-2d1a1b057570" />

-Chat Page(User1)
<img width="1858" height="960" alt="Screenshot 2025-11-02 173859" src="https://github.com/user-attachments/assets/1eb150e9-59a8-4b8c-ba94-20d21258fe81" />

-Chat Page(User2)
<img width="1866" height="970" alt="Screenshot 2025-11-02 173913" src="https://github.com/user-attachments/assets/dde7b3f2-6002-4024-81ce-79bb7fead17a" />


## Features

- Real-time messaging via hubs.  
- User-friendly web UI for chat.  
- C# backend with ASP.NET Core and SignalR.  
- Simple configuration via `appsettings.json`.  
- Ready to extend (e.g., authentication, persistent message store, etc.).

## Tech Stack

- **Backend**: ASP.NET Core 9.0 (C#)  
- **Real-time**: SignalR  
- **Frontend**: HTML / CSS / JavaScript (within the Views folder)  
- **Configuration**: `appsettings.json`, `appsettings.Development.json`  
- **Structure**: MVC architecture (Controllers, Hubs, Models, Services)  


## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/) (or whichever version your `csproj` uses)  
- A modern web browser  
- (Optional) Visual Studio or VS Code for development  

### Installation & Setup

1. Clone the repository  
   ```bash
   git clone https://github.com/tejaspawade/chatSphere.git
   cd chatApp
2. Review configuration in appsettings.json (and appsettings.Development.json) to ensure any required settings (e.g., connection strings, hub endpoints) are correct.
3. Restore dependencies and build the project
   ```bash
   dotnet restore
   dotnet build

### Running the Application
   ```bash
   dotnet run --project path/to/ChatApp.csproj

## Project Structure
  ```bash
  chatSphere/
  ├── Controllers/        # MVC Controllers  
  ├── Hubs/               # SignalR hubs  
  ├── Models/             # Domain/data models  
  ├── Services/           # Business logic and abstractions  
  ├── Views/              # Front-end views (HTML/CSS/JS)  
  ├── Properties/  
  ├── wwwroot/            # Static assets (css/js/images)  
  ├── ChatApp.csproj  
  └── Program.cs  
 

