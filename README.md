# TestRocket

Test rockets, robots, and other fun experiments.

## Solution #1 - TestRocket.sln **VS Code**

The Sample TestRocket projects requires .NET Core Preview 8 (v3.0.0-preview8).

To run this project in VS Code make sure you download and install the **SDK 3.0.100-preview8-013656**.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.0)

### To run the itc-local test runner

`ctrl + shift + p` then type `Run Task` then choose `trun` task.

or from terminal

```shell
cd out\Debug\netcoreapp3.0
itc-local
```

### To run xUnit tests

`ctrl + shift + p` then type `Run Task` then choose `test` task.

or if `test` is the default test task

`ctrl + shift + p` then type `Run All Tests` (`alt + r` `alt + a`)

or if you have installed .NET Core Test Explorer simply run from the window.

[.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)

### Steps Used in TestRockets Creation

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

## Solution #2 - ZipUtility.sln **Visual Studio 2019**

> Browse to and open:

\TestRocket\src\zip\ZipUtility\ZipUtility.sln

## Unit Testing Resources and Samples

[Losing $460m in 45 minutes](https://www.bugsnag.com/blog/bug-day-460m-loss)

[xunit.net](https://xunit.net/)

[xunit.net - Getting Started .NET Core](https://xunit.net/docs/getting-started/netcore/cmdline)

[xunit.net - Getting Started Visual Studio](https://xunit.net/docs/getting-started/netfx/visual-studio)

[Getting Started with Unit Testing - MS Docs](https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2019)

[Walkthrough - MS Docs](https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019)

[unit-test-intro-slides.pdf](unit-test-intro-slides.pdf)