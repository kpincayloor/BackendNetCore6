using Confluent.Kafka;

namespace KafkaApacheProject.Producer
{
    public class KafkaProducerConfig<Tk, Tv> : ProducerConfig
    {
        public string Topic { get; set; }
    }
}
