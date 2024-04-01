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

    [HttpPost("fila-basica")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> FilaBasica([FromBody] string mensagem)
    {
        var serviceBusSender = _serviceBusSenderProvider.Provide("fila-basica");
        await serviceBusSender.SendMessageAsync(new ServiceBusMessage(mensagem));

        return Accepted();
    }
}
