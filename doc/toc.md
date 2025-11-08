# TestRocket Solutions - Table of Contents

A collection of .NET Core solutions demonstrating various programming patterns, testing frameworks, and technologies.

## Solutions Overview

| Solution | Location | Description |
|----------|----------|-------------|
| [TestRocket.sln](../Rockets/src/TestRocket.sln) | `Rockets/src/` | Core rocket factory demonstration with xUnit testing framework |
| [Patterns.sln](../Patterns/Patterns.sln) | `Patterns/` | Gang of Four (GoF) design patterns implementations with tests |
| [MissionDb.sln](../MissionDb/MissionDb.sln) | `MissionDb/` | SQL Server database project with unit tests |
| [ZipUtility.sln](../ZipUtility/ZipUtility.sln) | `ZipUtility/` | Utility libraries with MSTest and xUnit experiments |
| [DapperApi.sln](../DapperApi/DapperApi.sln) | `DapperApi/` | Dapper ORM with ASP.NET Core, gRPC, and Web applications |
| [Lessons.sln](../Lessons/src/Lessons.sln) | `Lessons/src/` | C# programming lessons and examples |

---

## TestRocket.sln

**Location:** [`Rockets/src/TestRocket.sln`](../Rockets/src/TestRocket.sln)  
**Framework:** .NET Core 3.0+

### Projects

- **FactoryXUnit** - xUnit test project for the rocket factory
- **RocketFactoryTests** - Class library with additional rocket factory tests
- **itc** - Custom test console application
- **RocketWorker** - Background worker service for rocket processing
- **RocketConsole** - Console application demonstrating rocket functionality
- **RocketWriter** - Rocket data writing utilities
- **RocketFactory** - Core rocket factory class library
- **RenderConsole** - Console application for rendering demonstrations
- **RazorClassLibrary** - Razor template library for rendering

---

## Patterns.sln

**Location:** [`Patterns/Patterns.sln`](../Patterns/Patterns.sln)  
**Framework:** .NET Core 3.1+

Comprehensive implementations of Gang of Four design patterns with console applications and test projects.

### Design Pattern Projects

#### Creational Patterns
- **AbstractFactory** - Abstract Factory pattern implementation with logic library and tests
- **FactoryMethod** - Factory Method pattern with console app and logic library
- **Builder** - Builder pattern console application
- **Singleton** - Singleton pattern demonstration

#### Structural Patterns
- **Adapter** - Adapter pattern console application
- **Bridge** - Bridge pattern demonstration
- **Composite** - Composite pattern implementation
- **Decorator** - Decorator pattern console application
- **Proxy** - Proxy pattern with logic library and tests

#### Behavioral Patterns
- **State** - State pattern console application
- **State2** - Alternative state pattern implementation
- **Mediator** - Mediator pattern demonstration
- **Command** - Command pattern (folder structure)

#### Integration Projects
- **WinArchiveEditor** - Windows Forms archive editor
- **WpfArchiveEditor** - WPF archive editor application
- **ArchiveLogic** - Archive logic library
- **ArchiveData** - Archive data access library

---

## MissionDb.sln

**Location:** [`MissionDb/MissionDb.sln`](../MissionDb/MissionDb.sln)  
**Framework:** .NET Core 3.1+  
**Database:** SQL Server (SSDT)

### Projects

- **MissionControl** - SQL Server database project (requires SSDT tools)
- **MissionControlDbTests** - Database unit tests
- **MissionControlTests** - Additional unit tests for mission control
- **TestConsole** - Console application for testing database operations

---

## ZipUtility.sln

**Location:** [`ZipUtility/ZipUtility.sln`](../ZipUtility/ZipUtility.sln)  
**Framework:** .NET Core 3.0+

Experimental projects demonstrating MSTest, xUnit, and C# 8 language features.

### Projects

- **ZipLib** - Core zip utility library
- **XunitZipTests** - xUnit tests for zip functionality
- **OpenApiDemo** - OpenAPI/Swagger demonstration
- **zipper** - Console application for zip operations
- **moncon** - MongoDB console application
- **LogApi** - ASP.NET Core logging API
- **SRF.FileLogging** - File-based logging library
- **LogWorker** - Background logging worker service
- **SRF.CommandItems** - Command pattern utilities library
- **LogApiTestClient** - Test client for LogApi
- **SRF.BasicAuth** - Basic authentication library
- **iopipeline** - I/O pipeline processing library

---

## DapperApi.sln

**Location:** [`DapperApi/DapperApi.sln`](../DapperApi/DapperApi.sln)  
**Framework:** .NET Core 3.0+

Dapper micro-ORM demonstrations with various ASP.NET Core technologies.

### Projects

- **DapperApi** - ASP.NET Core Web API using Dapper
- **DapperTests** - xUnit tests for Dapper operations
- **DapperGRPC** - gRPC service with Dapper
- **GRPCClient** - Client for gRPC service
- **DapperWeb** - ASP.NET Core Web application with Dapper
- **Ticketer** - gRPC ticketing service
- **TicketerClient** - Client for ticketing service

---

## Lessons.sln

**Location:** [`Lessons/src/Lessons.sln`](../Lessons/src/Lessons.sln)  
**Framework:** .NET Core 3.1+

Programming lessons and educational projects.

### Projects

- **Array-Complete** - Completed array manipulation examples
- **Array-Start** - Starting point for array lessons

---

## Additional Resources

- **Documentation:** See `doc/` folder for articles, code review templates, and markdown guides
- **GitHub Repository:** [https://github.com/JohnPKosh/TestRocket](https://github.com/JohnPKosh/TestRocket)
- **License:** MIT
