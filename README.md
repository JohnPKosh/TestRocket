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