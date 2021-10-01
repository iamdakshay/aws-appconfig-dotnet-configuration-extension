## DotnetCore Configuration extension for AWS AppConfig

Amazon Secrets Manager service provides secure store for key/value pairs. Unlike parameter store, the data stored in Secrets Manager is encrypted by default. You can use an extension to load config values in application's Configuration object. You must have saved config values in JSON format against keys in Secrets Manager.
<br><br>

### Projects

- Amazon.Extensions.Configuration.AppConfig
- samples
- Amazon.Extensions.Configuration.AppConfig.Tests
  <br>

### Nuget packages

- Microsoft.Extensions.Configuration
- AWSSDK.AppConfig
- System.Text.Json
- Moq
- xunit
- Microsoft.NET.Test.Sdk
- xunit.runner.visualstudio
- coverlet.collector
  <br>

### Usage:

<b>Step 1</b>: Create application in AppConfig. Provide name as "AppConfigDemo". Create environment under project named as "Development". Create configuration iwth name, "queueConfigurationProfile".
<br>
Provide value of configuration as below.

```json
{ Kafka: { "BootstrapServers": "localhost", "Producer": { "ClientId": "19", "StatisticsIntervalMs": 5000, "MessageTimeoutMs": 10000, "SocketTimeoutMs": 10000, "ApiVersionRequestTimeoutMs": 10000, "MetadataRequestTimeoutMs": 5000, "RequestTimeoutMs": 5000 }, "Consumer": { "GroupId": "49", "EnableAutoCommit": true, "StatisticsIntervalMs": 5000, "SessionTimeoutMs": 10000 } },
RabbitMQ : { "Uri": "localhost", "UserName": "root@3", "Password": "SWAMI", "DispatchConsumersAsync": true } }
```

<br>
<b>Step 2</b>: Add reference of Amazon.Extensions.Configuration.AppConfig project. Add extesion in using following code in Program.cs
<br>
<br>


```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config
                .AddAWSAppConfigConfiguration("AppConfigDemo",
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                "queueConfigurationProfile",
                new Guid().ToString());
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```

<br>
<b>Step 3</b>: Access configuration in code.
<br>
Startup.cs - <i>Options pattern</i>

```csharp
services.Configure<KafkaConfig>(Configuration.GetSection("Kafka"));
services.Configure<RabbitMQConfig>(Configuration.GetSection("RabbitMQ"));
```