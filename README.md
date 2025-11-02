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

*(Add screenshots or link to a live demo if available)*

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
   cd chatSphere
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

