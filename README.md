## DotnetCore Configuration extension for AWS AppConfig
AWS AppConfig is a capability of AWS Systems Manager, to create, manage, and quickly deploy application configurations. You can store application configurations per environment and maitain versioned copies.

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
