<img src="elm/design/img/test-rocket-header.svg" alt="Logo" >

Test rockets, robots, and other fun experiments. Feel free to kick the tires and create something fun that you think could be contributed to this repo.

## Articles

### **["Agile Software Reboot"](doc/agile-software-reboot.pdf)**

### **["IT Mechanisms for Success"](doc/choose-your-weapon.pdf)** - _Don't bring a knife to a gunfight_

---

## Why is this project here?

Well, this project hopefully checks off **#9**, **#7**, and **#6** on my list of every day TODOs according to my professional oath.

### The Programmer's Oath by Robert C. Martin (Uncle Bob)

[blog.cleancoder.com](https://blog.cleancoder.com/uncle-bob/2015/11/18/TheProgrammersOath.html)

1. I will not produce harmful code.

2. The code that I produce will always be my best work. I will not knowingly allow code that is defective either in behavior or structure to accumulate.

3. I will produce, with each release, a quick, sure, and repeatable proof that every element of the code works as it should.

4. I will make frequent, small, releases so that I do not impede the progress of others.

5. I will fearlessly and relentlessly improve my creations at every opportunity. I will never degrade them.

6. I will do all that I can to keep the productivity of myself, and others, as high as possible. I will do nothing that decreases that productivity.

7. I will continuously ensure that others can cover for me, and that I can cover for them.

8. I will produce estimates that are honest both in magnitude and precision. I will not make promises without certainty.

9. I will never stop learning and improving my craft.

---

## Code Review Documents (WIP)

**["Team Code Review Template"](doc/code-review-template.md)**

---

## Solution #1 - TestRocket.sln

> The Sample solution require .NET Core 3.

To run this solution make sure you download and install the SDK.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.0)

> Browse to and open:

```shell
%GIT%\TestRocket\Rockets\src\TestRocket.sln
```

### To run the itc-local test runner

`ctrl + shift + p` then type `Run Task` then choose `trun` task.

or from terminal

```shell
cd %GIT%\TestRocket\Rockets\out\Debug\netcoreapp3.0
itc-local
```

### To run xUnit tests

`ctrl + shift + p` then type `Run Task` then choose `test` task.

or if `test` is the default test task

`ctrl + shift + p` then type `Run All Tests` (`alt + r` `alt + a`)

or if you have installed .NET Core Test Explorer simply run from the window.

[.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)

### Example Steps Used in TestRockets Creation

### Create Solution

```shell
dotnet new sln -o %GIT%\TestRocket\Rockets\src -n TestRocket
```

### List All Templates for `dotnet new`

```shell
dotnet new -l
``` 

### Create New RocketFactory Project

```shell
dotnet new classlib -o %GIT%\TestRocket\Rockets\src\RocketFactory -n RocketFactory
```

### Add RocketFactory Project to Solution

```shell
dotnet sln TestRocket.sln add %GIT%\TestRocket\Rockets\src\RocketFactory\RocketFactory.csproj
```

### Create New xUnit Test Project

```shell
dotnet new xunit -o %GIT%\TestRocket\Rockets\src\Test -n FactoryXUnit
```

### Add Project to Solution

```shell
dotnet sln TestRocket.sln add %GIT%\TestRocket\Rockets\src\Test\FactoryXUnit\FactoryXUnit.csproj
```

### Create New RocketFactoryTests Project

```shell
dotnet new classlib -o %GIT%\TestRocket\src\Test\Factory -n RocketFactoryTests
```

### Add RocketFactoryTests Project to Solution

```shell
dotnet sln TestRocket.sln add %GIT%\TestRocket\src\Test\Factory\RocketFactoryTests.csproj
```

---

## Solution #2 - Patterns.sln

> The Sample solution requires .NET Core 3.1.

This solution is a good place to see some XUnit tests specific to the GoF (Gang of Four) design patterns.

To run this solution make sure you download and install the SDK.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.1)

> Browse to and open:

```shell
%GIT%\TestRocket\Patterns\Patterns.sln
```

---

## Solution #3 - MissionDb.sln

> The Sample solution requires .NET Core 3.1.

This solution contains a SQL Server Project (You will need SSDT tools installed with VS) and Unit Test project for the Database.

To run this solution make sure you download and install the SDK.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.1)

> Browse to and open:

```shell
%GIT%\TestRocket\MissionDb\MissionDb.sln
```

---

## Solution #4 - ZipUtility.sln

> The Sample solution requires .NET Core 3.

To run this solution make sure you download and install the SDK.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.0)

This solution is a good place to review some MSTest, XUnit, and C# 8 language Unit Test experiments.

> Browse to and open:

```shell
%GIT%\TestRocket\ZipUtility\ZipUtility.sln
```

---

## Solution #5 - DapperApi.sln

> The Sample solution requires .NET Core 3.

To run this solution make sure you download and install the SDK.

[.NET Core SDK Download Page](https://dotnet.microsoft.com/download/dotnet-core/3.0)

> Browse to and open:

```shell
%GIT%\TestRocket\DapperApi\DapperApi.sln
```

---

## Unit Testing Resources and Samples

[Losing $460m in 45 minutes](https://www.bugsnag.com/blog/bug-day-460m-loss)

[xunit.net](https://xunit.net/)

[xunit.net - Getting Started .NET Core](https://xunit.net/docs/getting-started/netcore/cmdline)

[xunit.net - Getting Started Visual Studio](https://xunit.net/docs/getting-started/netfx/visual-studio)

[Getting Started with Unit Testing - MS Docs](https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2019)

[Walkthrough - MS Docs](https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019)

[unit-test-intro-slides.pdf](doc/unit-test-intro-slides.pdf)


---

## Markdown CheatSheet

**["markdown-cheatsheet.md"](doc/markdown-cheatsheet.md)**