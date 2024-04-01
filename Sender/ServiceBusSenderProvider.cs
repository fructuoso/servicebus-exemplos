using Azure.Messaging.ServiceBus;

namespace Sender;

public class ServiceBusSenderProvider
{
    private readonly ServiceBusClient _client;

    public ServiceBusSenderProvider(ServiceBusClient serviceBusClient)
    {
        _client = serviceBusClient;
    }

    public ServiceBusSender Provide(string entityPath)
    {
        return _client.CreateSender(entityPath);
    }
}
