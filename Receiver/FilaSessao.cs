using System.Text;
using Azure.Messaging.ServiceBus;

namespace Receiver;

public class FilaSessao : BackgroundService
{
    private readonly ILogger<FilaSessao> _logger;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly string _queueName = "fila-sessao";

    public FilaSessao(ILogger<FilaSessao> logger, ServiceBusClient serviceBusClient)
    {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Create a Service Bus processor for the queue
        var processor = _serviceBusClient.CreateSessionProcessor(_queueName, new ServiceBusSessionProcessorOptions() { MaxConcurrentSessions = 1, SessionIdleTimeout = TimeSpan.FromSeconds(5) });

        // Register the message handler
        processor.ProcessMessageAsync += ProcessMessageAsync;
        processor.ProcessErrorAsync += ProcessErrorAsync;

        // Start processing messages
        await processor.StartProcessingAsync(stoppingToken);

        // Wait for the stopping token to be triggered
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        // Stop processing messages
        await processor.StopProcessingAsync(stoppingToken);
    }

    private async Task ProcessMessageAsync(ProcessSessionMessageEventArgs args)
    {
        // Process the received message
        string body = Encoding.UTF8.GetString(args.Message.Body);
        _logger.LogInformation("Mensagem recebida na fila '{fila}' da sessão '{sessao}' com conteúdo: {conteudo}", _queueName, args.Message.SessionId , body);
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs args)
    {
        // Handle any errors that occur during message processing
        _logger.LogError("Error occurred: {exception}", args.Exception);

        return Task.CompletedTask;
    }
}