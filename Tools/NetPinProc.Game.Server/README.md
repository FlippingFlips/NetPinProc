# NetPinProc.Game.Server
## ❓ What is it?
Managing NetPinProc game and data through a web server hosted on pinball machine.
Client pages written in Blazor using MudBlazor material design controls library.

## ⚡ Getting Started
Setup a path to local game database `netproc.db` file in the `Server/appsettings.json`, defaults to `C:\netproc.db`

## 🔧 Building and Running
- Setup nuget source to download packages or use packages locally

* (Client) NetPinProc.Domain
* (Server) NetPinProc.Game.Sqlite

- Run the server project any machine with `dotnet`
- Host as service with `dotnet` on linux (todo)

![](screen1.jpg)