# GovGet

GovGet is a practice project for learning .NET by retrieving and working with data from free, publicly available government APIs.

## Purpose

The goal of GovGet is to learn how to leverage .NET across different application interfaces, including:

* CLI
* Web (local and cloud)
* Desktop
* Mobile

These are planned targets rather than guaranteed deliverables. Development will primarily focus on the CLI and web application.

## Prerequisites

* .NET SDK 10.0 or later
* Node.js and npm

## Run the CLI

From the repository root, run the CLI with `dotnet run`:

```bash
dotnet run --project src/GovGet.Cli -- help
dotnet run --project src/GovGet.Cli -- ping
dotnet run --project src/GovGet.Cli -- usgs help
dotnet run --project src/GovGet.Cli -- usgs version
dotnet run --project src/GovGet.Cli -- usgs count
```

The `usgs count` command retrieves the number of earthquakes recorded by the USGS in the last 30 days.

## Run the API

Start the ASP.NET API from the repository root:

```bash
dotnet run --project src/GovGet.Api
```

The API runs at `http://localhost:5148` by default. Available endpoints include:

```text
GET http://localhost:5148/api/ping
GET http://localhost:5148/api/count
```

For example:

```bash
curl http://localhost:5148/api/ping
curl http://localhost:5148/api/count
```

## Run the web application

The web application uses Vite and expects the API to be running at `http://localhost:5148`.

1. In one terminal, start the API as shown above.
2. In a second terminal, install the web dependencies and start the development server:

```bash
cd src/GovGet.Web
npm install
npm run dev
```

Open the URL printed by Vite, usually `http://localhost:5173`. The Vite development server proxies the web app's `/api` requests to the local ASP.NET API.
