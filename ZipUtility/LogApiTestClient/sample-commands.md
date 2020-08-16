# Command Line Samples

Open a command propt in the API deployment directory:

```shell
cd C:\vscode\github\TestRocket\ZipUtility\LogApi\bin\Release\netcoreapp3.1
```

Start the API:

```shell
LogApi.exe
```

Open a command prompt in the API client deployment directory:

```shell
cd C:\vscode\github\TestRocket\ZipUtility\LogApiTestClient\bin\Release\netcoreapp3.1
```

To execute GET method n times:

```shell
LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices" -n 25
```

```shell
LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices/disable" -n 25
```

```shell
LogApiTestClient get --url "https://localhost:5001/sys/backgroundservices/enable" -n 25
```


```shell
LogApiTestClient get --url "https://localhost:5001/weatherforecast" -n 25
```