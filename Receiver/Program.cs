using System.Text.Json;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Receiver;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<FilaBasicaWorker>();
builder.Services.AddHostedService<FilaSemConfirmacaoWorker>();
builder.Services.AddHostedService<FilaTimeoutWorker>();
builder.Services.AddHostedService<FilaAteUmaVezWorker>();

builder.Logging.AddJsonConsole(options =>
{
    options.IncludeScopes = true;
    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
    options.JsonWriterOptions = new JsonWriterOptions
    {
        Indented = true
    };
});

builder.Services.AddSingleton<ServiceBusClient>(_ =>
{
    var serviceBusNamespace = _.GetService<IConfiguration>()["ServiceBus:Namespace"];
    return new ServiceBusClient(serviceBusNamespace, new DefaultAzureCredential());
});

var host = builder.Build();
host.Run();

