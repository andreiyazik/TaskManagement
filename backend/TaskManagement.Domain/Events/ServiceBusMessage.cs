using Newtonsoft.Json.Linq;

namespace TaskManagement.Domain.Events;

public class ServiceBusMessage
{
    public string Name { get; set; }
    public JObject Payload { get; set; }
}
