using Amazon;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;
using Microsoft.Extensions.Configuration;

namespace Amazon.Extensions.Configuration.AppConfig
{
    public class AWSAppConfigSource : IConfigurationSource
    {
        private readonly string _application;

        private readonly string _environment;

        private readonly string _configuration;

        private readonly IAmazonAppConfig _appConfig;

        private readonly string _clientId;

        public AWSAppConfigSource(
            string application,
            string environment,
            string configuration,
            string clientId
        )
        {
            _application = application;
            _environment = environment;
            _configuration = configuration;
            _appConfig = new AmazonAppConfigClient();
            _clientId = clientId;
        }

        public AWSAppConfigSource(
            string application,
            string environment,
            string configuration,
            string clientId,
            IAmazonAppConfig appConfig
        )
        {
            _application = application;
            _environment = environment;
            _configuration = configuration;
            _appConfig = appConfig;
            _clientId = clientId;
        }

        public AWSAppConfigSource(
            string application,
            string environment,
            string configuration,
            string clientId,
            string region
        )
        {
            _application = application;
            _environment = environment;
            _configuration = configuration;
            _appConfig =
                new AmazonAppConfigClient(RegionEndpoint
                        .GetBySystemName(region));
            _clientId = clientId;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AWSAppConfigConfigurationProvider(_appConfig,
                _application,
                _environment,
                _configuration,
                _clientId);
        }
    }
}
