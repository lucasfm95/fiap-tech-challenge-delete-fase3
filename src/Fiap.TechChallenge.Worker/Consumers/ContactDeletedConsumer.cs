using Fiap.TechChallenge.Application.Services.Interfaces;
using Fiap.TechChallenge.LibDomain.Events;
using MassTransit;
using Prometheus;

namespace Fiap.TechChallenge.Worker.Consumers;

public class ContactDeletedConsumer(ILogger<ContactDeletedConsumer> logger, IContactService contactService)
    : IConsumer<ContactDeleteEvent>
{
    private static readonly Counter ProcessedJobsCounter =
        Metrics.CreateCounter("deleted_contact_total_processed", "Number of Contact Deleted consumed.");
    public async Task Consume(ConsumeContext<ContactDeleteEvent> context)
    {
        try
        {
            //Prometheus metrics
            ProcessedJobsCounter.Inc();
            
            var result = await contactService.DeleteAsync(context.Message.Id, context.CancellationToken);
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