using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace Sender.Controllers;

[ApiController]
[Route("[controller]")]
public class SenderController : ControllerBase
{
    private readonly ILogger<SenderController> _logger;
    private readonly ServiceBusSenderProvider _serviceBusSenderProvider;

    public SenderController(ILogger<SenderController> logger, ServiceBusSenderProvider serviceBusSenderProvider)
    {
        _logger = logger;
        _serviceBusSenderProvider = serviceBusSenderProvider;
    }

    [HttpPost("{entidade}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Enviar(string entidade, [FromBody] string mensagem)
    {
        var serviceBusSender = _serviceBusSenderProvider.Provide(entidade);
        await serviceBusSender.SendMessageAsync(new ServiceBusMessage(mensagem));

        return Accepted();
    }

    [HttpPost("particao/{entidade}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> EnviarComParticao(string entidade, [FromBody] string mensagem)
    {
        var serviceBusSender = _serviceBusSenderProvider.Provide(entidade);

        var messages = new List<ServiceBusMessage>();

        // Envia 50 mensagens para cada partição A

        for (int m = 0; m < 50; m++)
        {
            messages.Add(new ServiceBusMessage($"{mensagem} - {m}") { PartitionKey = "particao-a" });
        }

        await SendMessagesInBatch(serviceBusSender, messages);


        // Envia 50 mensagens para cada partição B

        messages.Clear();
        for (int m = 0; m < 50; m++)
        {
            messages.Add(new ServiceBusMessage($"{mensagem} - {m}") { PartitionKey = "particao-b" });
        }

        await SendMessagesInBatch(serviceBusSender, messages);

        // Envia 50 mensagens para cada partição A

        messages.Clear();
        for (int m = 50; m < 100; m++)
        {
            messages.Add(new ServiceBusMessage($"{mensagem} - {m}") { PartitionKey = "particao-a" });
        }

        await SendMessagesInBatch(serviceBusSender, messages);

        return Accepted();
    }

    private async Task SendMessagesInBatch(ServiceBusSender serviceBusSender, List<ServiceBusMessage> messages)
    {
        var messageBatch = await serviceBusSender.CreateMessageBatchAsync();
        foreach (var message in messages)
        {
            if (!messageBatch.TryAddMessage(message))
            {
                await serviceBusSender.SendMessagesAsync(messageBatch);
                messageBatch = await serviceBusSender.CreateMessageBatchAsync();
                messageBatch.TryAddMessage(message);
            }
        }

        if (messageBatch.Count > 0)
        {
            await serviceBusSender.SendMessagesAsync(messageBatch);
        }
    }
}
