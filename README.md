# TestRocket
Test rockets, robots, and other fun experiments.

## Manual Setup

### Create Solution

```shell
dotnet new sln -n TestRocket
```

### Add a Project

```shell
dotnet new classlib -o C:\vscode\github\TestRocket\src\Factory -n RocketFactory
```

### Add Project to Solution

```shell
dotnet sln TestRocket.sln add C:\vscode\github\TestRocket\src\Factory\RocketFactory.csproj
```

### Add a xUnit Test Project

```shell
dotnet new xunit -n FactoryXUnit
```


### Add Project to Solution

```shell
dotnet sln TestRocket.sln add C:\VsCode\github\TestRocket\src\Test\FactoryXUnit\FactoryXUnit\FactoryXUnit.csproj
```


### Add a Project

```shell
dotnet new classlib -o C:\vscode\github\TestRocket\src\Test\Factory -n RocketFactoryTests
```


### Add Project to Solution

```shell
dotnet sln TestRocket.sln add C:\VsCode\github\TestRocket\src\Test\Factory\RocketFactoryTests.csproj
```
