using System;

namespace Samples
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public KafkaProducerConfig Producer { get; set; }
        public KafkaConsumerConfig Consumer { get; set; }
    }

    public class KafkaProducerConfig
    {
        public string ClientId { get; set; }
        public int StatisticsIntervalMs { get; set; }
        public int MessageTimeoutMs { get; set; }
        public int SocketTimeoutMs { get; set; }
        public int ApiVersionRequestTimeoutMs { get; set; }
        public int MetadataRequestTimeoutMs { get; set; }
        public int RequestTimeoutMs { get; set; }
    }

    public class KafkaConsumerConfig
    {
        public string GroupId { get; set; }
        public bool EnableAutoCommit { get; set; }
        public int StatisticsIntervalMs { get; set; }
        public int SessionTimeoutMs { get; set; }
    }
}
