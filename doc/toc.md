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

```cmd
REM Open solution in Visual Studio
start %DEV%\TestRocket\Rockets\src\TestRocket.sln

REM Navigate to solution directory
cd /d %DEV%\TestRocket\Rockets\src

REM Restore NuGet packages
dotnet restore

REM Build the entire solution (add --no-restore to skip restore)
dotnet build

REM Build in Release mode
dotnet build -c Release

REM Run tests (xUnit and custom itc tests)
dotnet test

REM Run specific test project
dotnet test Test\FactoryXUnit\FactoryXUnit.csproj

REM Run the RocketConsole application
dotnet run --project RocketConsole\RocketConsole.csproj

REM Run the RocketWorker background service
dotnet run --project RocketWorker\RocketWorker.csproj

REM Clean build artifacts
dotnet clean

REM Publish for deployment
dotnet publish -c Release -o .\publish
```

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

```cmd
REM Open solution in Visual Studio
start %DEV%\TestRocket\Patterns\Patterns.sln

REM Navigate to solution directory
cd /d %DEV%\TestRocket\Patterns

REM Restore and build
dotnet restore
dotnet build

REM Run all tests (includes pattern implementation tests)
dotnet test

REM Run specific pattern console applications
dotnet run --project AbstractFactory\App\abstractfactory.csproj
dotnet run --project FactoryMethod\App\factorymethod.csproj
dotnet run --project Proxy\App\proxy.csproj
dotnet run --project Adapter\App\adapt.csproj
dotnet run --project Bridge\App\bridge.csproj
dotnet run --project Composite\App\compose.csproj
dotnet run --project Decorator\App\decorate.csproj
dotnet run --project Mediator\App\mediate.csproj
dotnet run --project Singleton\App\single.csproj
dotnet run --project State\App\alterstate.csproj
dotnet run --project State2\App\alterstate2\alterstate2.csproj
dotnet run --project Builder\App\build\build.csproj

REM Run specific test projects
dotnet test AbstractFactory\Tests\AbstractFactoryTests\AbstractFactoryTests.csproj
dotnet test FactoryMethod\Tests\FactoryMethodTests\FactoryMethodTests.csproj
dotnet test Proxy\Tests\ProxyTests\ProxyTests.csproj

REM Build specific pattern with verbose output
dotnet build AbstractFactory\App\abstractfactory.csproj -v detailed
```

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

```cmd
REM Open solution in Visual Studio (includes SQL Server project)
start %DEV%\TestRocket\MissionDb\MissionDb.sln

REM Navigate to solution directory
cd /d %DEV%\TestRocket\MissionDb

REM Restore and build .NET projects only (SQL project builds in VS)
dotnet restore
dotnet build

REM Run database unit tests
dotnet test DbTests\MissionControlDbTests.csproj

REM Run general unit tests
dotnet test MissionControlTests\MissionControlTests.csproj

REM Run all tests with detailed output
dotnet test --verbosity detailed

REM Run TestConsole application
dotnet run --project TestConsole\TestConsole.csproj

REM Note: SQL Server project (MissionControl.sqlproj) requires Visual Studio with SSDT
REM Build SQL project: msbuild Db\MissionControl.sqlproj /t:Build /p:Configuration=Debug
REM Publish SQL project: msbuild Db\MissionControl.sqlproj /t:Publish
```

### Projects

- **MissionControl** - SQL Server database project (requires SSDT tools)
- **MissionControlDbTests** - Database unit tests
- **MissionControlTests** - Additional unit tests for mission control
- **TestConsole** - Console application for testing database operations

---

## ZipUtility.sln

**Location:** [`ZipUtility/ZipUtility.sln`](../ZipUtility/ZipUtility.sln)  
**Framework:** .NET Core 3.0+

```cmd
REM Open solution in Visual Studio
start %DEV%\TestRocket\ZipUtility\ZipUtility.sln

REM Navigate to solution directory
cd /d %DEV%\TestRocket\ZipUtility

REM Restore and build
dotnet restore
dotnet build

REM Run xUnit tests
dotnet test Test\XunitZipTests\XunitZipTests.csproj

REM Run zipper console application
dotnet run --project zipper\zipper.csproj

REM Run MongoDB console application
dotnet run --project moncon\moncon.csproj

REM Run LogApi web API (starts Kestrel server)
dotnet run --project LogApi\LogApi.csproj

REM Run LogWorker background service
dotnet run --project LogWorker\LogWorker.csproj

REM Run OpenAPI demo application
dotnet run --project OpenApiDemo\OpenApiDemo.csproj

REM Run LogApiTestClient to test the API
dotnet run --project LogApiTestClient\LogApiTestClient.csproj

REM Build specific library projects
dotnet build ZipLib\ZipLib.csproj
dotnet build Logging\SRF.FileLogging.csproj
dotnet build SRF.CommandItems\SRF.CommandItems.csproj
dotnet build SRF.BasicAuth\SRF.BasicAuth.csproj

REM Watch for changes and rebuild (useful during development)
dotnet watch --project LogApi\LogApi.csproj run

REM Pack library as NuGet package
dotnet pack ZipLib\ZipLib.csproj -c Release -o .\nupkgs
```

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

```cmd
REM Open solution in Visual Studio
start %DEV%\TestRocket\DapperApi\DapperApi.sln

REM Navigate to solution directory
cd /d %DEV%\TestRocket\DapperApi

REM Restore and build
dotnet restore
dotnet build

REM Run xUnit tests for Dapper operations
dotnet test DapperTests\DapperTests.csproj

REM Run DapperApi web API (REST endpoints)
dotnet run --project DapperApi\DapperApi.csproj

REM Run DapperApi with specific launch profile
dotnet run --project DapperApi\DapperApi.csproj --launch-profile DapperApi

REM Run DapperGRPC service
dotnet run --project DapperGRPC\DapperGRPC.csproj

REM Run gRPC client to test DapperGRPC
dotnet run --project GRPCClient\GRPCClient.csproj

REM Run DapperWeb application (Razor Pages)
dotnet run --project DapperWeb\DapperWeb.csproj

REM Run Ticketer gRPC service
dotnet run --project Ticketer\Ticketer.csproj

REM Run TicketerClient
dotnet run --project TicketerClient\TicketerClient.csproj

REM Watch and auto-rebuild API during development
dotnet watch --project DapperApi\DapperApi.csproj run

REM Build with specific framework version
dotnet build DapperApi\DapperApi.csproj -f netcoreapp3.0

REM Publish web API for deployment
dotnet publish DapperApi\DapperApi.csproj -c Release -o .\publish\api

REM Publish with runtime identifier (self-contained)
dotnet publish DapperApi\DapperApi.csproj -c Release -r win-x64 --self-contained
```

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

```cmd
REM Open solution in Visual Studio
start %DEV%\TestRocket\Lessons\src\Lessons.sln

REM Navigate to solution directory
cd /d %DEV%\TestRocket\Lessons\src

REM Restore and build
dotnet restore
dotnet build

REM Run the starter project (practice exercises)
dotnet run --project Arrays\Start\Array-Start.csproj

REM Run the completed project (reference solutions)
dotnet run --project Arrays\Complete\Array-Complete.csproj

REM Build starter project only
dotnet build Arrays\Start\Array-Start.csproj

REM Build completed project only
dotnet build Arrays\Complete\Array-Complete.csproj

REM Open solution filter for Array lessons only
start Array-Start.slnf
start Array-Complete.slnf

REM Compare starter vs completed implementations
REM (manually review code differences in VS or use git diff)
```

Programming lessons and educational projects.

### Projects

- **Array-Complete** - Completed array manipulation examples
- **Array-Start** - Starting point for array lessons

---

## Git Commands

Common Git operations for the TestRocket repository.

```bash
# Navigate to repository root
cd /d/devhome/ghub/TestRocket
# Or using Windows environment variable
cd $DEV/TestRocket

# Check current status
git status

# View current branch
git branch

# View all branches (local and remote)
git branch -a

# Stage all changes for commit
git add .

# Stage specific file
git add doc/toc.md

# Commit staged changes with message
git commit -m "Add table of contents for solutions"

# Stage and commit all tracked files in one command
git commit -am "Update documentation and examples"

# Push changes to remote repository (trunk branch)
git push origin trunk

# Push current branch
git push

# Pull latest changes from remote
git pull origin trunk

# Fetch all remote branches without merging
git fetch --all

# Fetch and prune deleted remote branches
git fetch --all --prune

# Merge changes from remote main/master into current branch
git merge origin/main
git merge origin/master

# View commit history
git log --oneline --graph --decorate

# View last 10 commits
git log -10

# Create a new branch
git branch feature/new-pattern

# Switch to a branch
git checkout trunk
git checkout feature/new-pattern

# Create and switch to new branch in one command
git checkout -b feature/builder-pattern

# Switch branches (newer syntax)
git switch trunk
git switch -c feature/new-feature

# View remote repositories
git remote -v

# Clone the repository (for new setup)
git clone https://github.com/JohnPKosh/TestRocket.git
git clone https://github.com/JohnPKosh/TestRocket.git TestRocket-Clone

# Clone specific branch
git clone -b trunk https://github.com/JohnPKosh/TestRocket.git

# View changes before staging
git diff

# View staged changes
git diff --staged

# View changes for specific file
git diff doc/toc.md

# Unstage a file
git restore --staged doc/toc.md

# Discard local changes to a file
git restore doc/toc.md

# Stash current changes
git stash
git stash save "WIP: working on patterns documentation"

# List stashes
git stash list

# Apply most recent stash
git stash apply

# Apply and remove most recent stash
git stash pop

# Delete a branch (local)
git branch -d feature/completed-feature

# Force delete a branch (local)
git branch -D feature/abandoned-feature

# Delete a remote branch
git push origin --delete feature/old-feature

# Update local list of remote branches
git remote update origin --prune

# View configuration
git config --list

# Set user name and email (if not set globally)
git config user.name "Your Name"
git config user.email "your.email@example.com"

# Create a tag
git tag v1.0.0
git tag -a v1.0.0 -m "Version 1.0.0 release"

# Push tags to remote
git push --tags

# Reset to specific commit (keep changes)
git reset --soft HEAD~1

# Reset to specific commit (discard changes)
git reset --hard HEAD~1

# Revert a commit (creates new commit)
git revert <commit-hash>
```

---

## Additional Resources

- **Documentation:** See `doc/` folder for articles, code review templates, and markdown guides
- **GitHub Repository:** [https://github.com/JohnPKosh/TestRocket](https://github.com/JohnPKosh/TestRocket)
- **License:** MIT
