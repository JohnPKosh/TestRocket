# TestRocket

Test rockets, robots, and other fun experiments.

## Requirements

This project requires .NET Core Preview 8 (v3.0.0-preview8).

To run this project in VS Code make sure you download and install the **SDK 3.0.100-preview8-013656**.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## To run the itc-local test runner

`ctrl + shift + p` then type `Run Task` then choose `trun` task.

or from terminal

```shell
cd out\Debug\netcoreapp3.0
itc-local
```

## To run xUnit tests

`ctrl + shift + p` then type `Run Task` then choose `test` task.

or if `test` is the default test task

`ctrl + shift + p` then type `Run All Tests` (`alt + r` `alt + a`)

or if you have installed .NET Core Test Explorer simply run from the window.

[.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)

## Manual Setup

### Create Solution

```shell
dotnet new sln -n TestRocket
```

### Create New RocketFactory Project

```shell
dotnet new classlib -o C:\vscode\github\TestRocket\src\Factory -n RocketFactory
```

### Add RocketFactory Project to Solution

```shell
dotnet sln TestRocket.sln add C:\vscode\github\TestRocket\src\Factory\RocketFactory.csproj
```

### Create New xUnit Test Project

```shell
dotnet new xunit -n FactoryXUnit
```

### Add Project to Solution

```shell
dotnet sln TestRocket.sln add C:\VsCode\github\TestRocket\src\Test\FactoryXUnit\FactoryXUnit\FactoryXUnit.csproj
```

### Create New RocketFactoryTests Project

```shell
dotnet new classlib -o C:\vscode\github\TestRocket\src\Test\Factory -n RocketFactoryTests
```

### Add RocketFactoryTests Project to Solution

```shell
dotnet sln TestRocket.sln add C:\VsCode\github\TestRocket\src\Test\Factory\RocketFactoryTests.csproj
```
