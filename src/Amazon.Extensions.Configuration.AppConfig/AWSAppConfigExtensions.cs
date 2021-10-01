using System;
using Amazon;
using Microsoft.Extensions.Configuration;

namespace Amazon.Extensions.Configuration.AppConfig
{
    public static class AWSAppConfigExtensions
    {
        public static IConfigurationBuilder
        AddAWSAppConfigConfiguration(
            this IConfigurationBuilder builder,
            string application,
            string environment,
            string configuration,
            string clientId
        )
        {
            return builder
                .Add(new AWSAppConfigSource(application,
                    environment,
                    configuration,
                    clientId));
        }
    }
}
