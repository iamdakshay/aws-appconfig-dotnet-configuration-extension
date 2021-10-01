- ### To create class lib project

```
dotnet new classlib \
--name Amazon.Extensions.Configuration.AppConfig \
--framework "netstandard2.0"
```

<br>

- ### To add required packages to extension project

```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package AWSSDK.AppConfig
dotnet add package System.Text.Json
```

<br>

- ### To build classlib

```
dotnet build
```

<br>

- ### To create sample API project

```
dotnet new webapi \
--name Samples \
--framework netcoreapp3.1
```

<br>

- ### To add reference of configuration project to sample project

```
dotnet add reference ../src/Amazon.Extensions.Configuration.AppConfig/Amazon.Extensions.Configuration.AppConfig.csproj
```

<br>

- ### To add Options package to sample API project

```
dotnet add package Microsoft.Extensions.Options
```

<br>

- ### To run sample application

```
dotnet run
```

<br>

- ### To create test project

```
dotnet new xunit \
--name Amazon.Extensions.Configuration.AppConfig.Tests \
--framework netcoreapp3.1
```

- ### To add reference of configuration project to test project

```
dotnet add reference ../../src/Amazon.Extensions.Configuration.AppConfig/Amazon.Extensions.Configuration.AppConfig.csproj
```

<br>

- ### To add required packages to test project

```
dotnet add package AWSSDK.AppConfig
dotnet add package Moq
```

<br>

- ### To create solution file

```
dotnet new sln \
--name Amazon.Extensions.Configuration.AWSAppConfig
```

<br>

- ### To add configuration extension project and test project to solution file

```
dotnet sln add src/Amazon.Extensions.Configuration.AppConfig/Amazon.Extensions.Configuration.AppConfig.csproj
dotnet sln add test/Amazon.Extensions.Configuration.AppConfig.Tests/Amazon.Extensions.Configuration.AppConfig.Tests.csproj
```

<br>
