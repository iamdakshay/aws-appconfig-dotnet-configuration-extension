using System;
using System.Collections.Generic;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;
using Microsoft.Extensions.Configuration;

namespace Amazon.Extensions.Configuration.AppConfig
{
    public class AWSAppConfigConfigurationProvider : ConfigurationProvider
    {
        private readonly string _application;

        private readonly string _environment;

        private readonly string _configuration;

        private readonly IAmazonAppConfig _appConfig;

        private readonly string _clientId;

        public AWSAppConfigConfigurationProvider(
            IAmazonAppConfig appConfig,
            string application,
            string environment,
            string configuration,
            string clientId
        )
        {
            _application = application;
            _environment = environment;
            _configuration = configuration;
            _appConfig = appConfig;
            _clientId = clientId;
        }

        public override void Load()
        {
            var request =
                new GetConfigurationRequest {
                    Application = _application,
                    Environment = _environment,
                    Configuration = _configuration,
                    ClientId = _clientId.ToString()
                };
            var appConfigResponse =
                _appConfig.GetConfigurationAsync(request).Result;

            var configString =
                MemoryStreamDecoder
                    .DecodeMemoryStreamToString(appConfigResponse.Content);

            Console.WriteLine($"data:{configString}");

            var data = new JsonConfigurationParser().Parse(configString);
            Data = data;
        }
    }
}
