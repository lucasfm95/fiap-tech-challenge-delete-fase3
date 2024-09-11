using ContractMessage;
using Fiap.TechChallenge.Application.Services.Interfaces;
using MassTransit;

namespace Fiap.TechChallenge.Delete.Fase3.Worker.Consumers;

public class ContactDeletedConsumer(ILogger<ContactDeletedConsumer> logger, IContactService contactService)
    : IConsumer<ContactDeleted>
{
    public async Task Consume(ConsumeContext<ContactDeleted> context)
    {
        try
        {
            var result= await contactService.DeleteAsync(context.Message.Id, context.CancellationToken);
            if (!result)
            {
                logger.LogWarning("Contact with id {Id} not found", context.Message.Id);
                return;
            }
            logger.LogInformation("Removed contact with id {Id}", context.Message.Id);
        }
        catch (Exception e)
        {
            logger.LogError("Error removing contact with id {Id}: {Message}", context.Message.Id, e.Message);
        }
    }
}