using System;

namespace Samples
{
    public class RabbitMQConfig
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool DispatchConsumersAsync { get; set; }
    }
}
