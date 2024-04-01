using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Receiver;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<ServiceBusClient>(_ =>
{
    var serviceBusNamespace = _.GetService<IConfiguration>()["ServiceBus:Namespace"];
    return new ServiceBusClient(serviceBusNamespace, new DefaultAzureCredential());
});

var host = builder.Build();
host.Run();

