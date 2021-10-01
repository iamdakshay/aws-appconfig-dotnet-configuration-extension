using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;
using Moq;
using Xunit;
using System.Text.Json;

namespace Amazon.Extensions.Configuration.AppConfig.Tests
{
    public class AWSAppConfigConfigurationProviderTest
    {
        [Theory]
        [MemberData(nameof(ParametersData))]
        public void LoadParametersTest(
            IAmazonAppConfig appConfig,
            IDictionary<string, string> configData
        )
        {
            AWSAppConfigConfigurationProvider awsAppConfigConfigurationProvider =
                new AWSAppConfigConfigurationProvider(appConfig,
                    "",
                    "",
                    "",
                    "");

            awsAppConfigConfigurationProvider.Load();
            foreach (var data in configData)
            {
                string value = string.Empty;
                awsAppConfigConfigurationProvider.TryGet(data.Key, out value);
                Assert.Equal(data.Value, value);
            }
        }

        public static TheoryData<IAmazonAppConfig, IDictionary<string, string>>
        ParametersData =>
            new TheoryData<IAmazonAppConfig, IDictionary<string, string>>()
            {
                {
                    GetAmazonAppConfig("JSONConfig"),
                    new Dictionary<string, string>()
                    { ["Kafka:Producer:ClientId"] = "19" }
                },
                {
                    GetAmazonAppConfig("EmptyConfig"),
                    new Dictionary<string, string>()
                    { ["Kafka:Producer:ClientId"] = null }
                },
                {
                    GetAmazonAppConfig("StringConfig"),
                    new Dictionary<string, string>()
                    { ["ConnectionString"] = "ConnectionStringValue" }
                }
            };

        private static IAmazonAppConfig GetAmazonAppConfig(string key)
        {
            Mock<IAmazonAppConfig> amazonAppConfig = null;

            switch (key)
            {
                case "JSONConfig":
                    amazonAppConfig = new Mock<IAmazonAppConfig>();
                    amazonAppConfig
                        .Setup(x =>
                            x
                                .GetConfigurationAsync(It
                                    .IsAny<GetConfigurationRequest>(),
                                It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new GetConfigurationResponse()
                        { Content = GetMemoryStream(JSONConfig) });
                    break;
                case "EmptyConfig":
                    amazonAppConfig = new Mock<IAmazonAppConfig>();
                    amazonAppConfig
                        .Setup(x =>
                            x
                                .GetConfigurationAsync(It
                                    .IsAny<GetConfigurationRequest>(),
                                It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new GetConfigurationResponse()
                        { Content = GetMemoryStream(EmptyConfig) });
                    break;
                case "StringConfig":
                    amazonAppConfig = new Mock<IAmazonAppConfig>();
                    amazonAppConfig
                        .Setup(x =>
                            x
                                .GetConfigurationAsync(It
                                    .IsAny<GetConfigurationRequest>(),
                                It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new GetConfigurationResponse()
                        { Content = GetMemoryStream(StringConfig) });
                    break;
                default:
                    amazonAppConfig = null;
                    break;
            }
            return amazonAppConfig.Object;
        }

        private const string
            JSONConfig
            =
            "{\"Kafka\": { \"BootstrapServers\": \"localhost\", \"Producer\": { \"ClientId\": \"19\", \"StatisticsIntervalMs\": 5000, \"MessageTimeoutMs\": 10000, \"SocketTimeoutMs\": 10000, \"ApiVersionRequestTimeoutMs\": 10000, \"MetadataRequestTimeoutMs\": 5000, \"RequestTimeoutMs\": 5000 }, \"Consumer\": { \"GroupId\": \"49\", \"EnableAutoCommit\": true, \"StatisticsIntervalMs\": 5000, \"SessionTimeoutMs\": 10000 } }, \"RabbitMQ\": { \"Uri\": \"localhost\", \"UserName\": \"root@3\", \"Password\": \"SWAMI\", \"DispatchConsumersAsync\": true } }";

        private const string EmptyConfig = "{}";

        private const string
            StringConfig
            =
            "{\"ConnectionString\": \"ConnectionStringValue\",\"CacheData\": true}";

        private static MemoryStream GetMemoryStream(string value)
        {
            byte[] byteArray = Encoding.Default.GetBytes(value);

            return new MemoryStream(byteArray);
        }
    }
}
