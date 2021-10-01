using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Samples.Controllers
{
    /// <summary>
    /// API to get configuration
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly KafkaConfig _kafkaConfig;
        private readonly RabbitMQConfig _rabbitMQConfig;

        public ConfigController(IOptions<KafkaConfig> kafkaConfig, IOptions<RabbitMQConfig> rabbitMQConfig)
        {
            _kafkaConfig = kafkaConfig.Value;
            _rabbitMQConfig = rabbitMQConfig.Value;
        }

        [HttpGet]
        [Route("GetKafkaConfig")]
        public async Task<IActionResult> GetKafkaConfig()
        {
            return Ok(_kafkaConfig);
        }

        [HttpGet]
        [Route("GetRabbitMQConfig")]
        public async Task<IActionResult> GetRabbitMQConfig()
        {
            return Ok(_rabbitMQConfig);
        }
    }
}