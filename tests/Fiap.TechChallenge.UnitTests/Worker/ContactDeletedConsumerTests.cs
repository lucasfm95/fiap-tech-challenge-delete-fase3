using Fiap.TechChallenge.Application.Services.Interfaces;
using Fiap.TechChallenge.Domain;
using Fiap.TechChallenge.LibDomain.Events;
using Fiap.TechChallenge.Worker.Consumers;
using MassTransit.Testing;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.TechChallenge.UnitTests.Worker;

public class ContactDeletedConsumerTests
{
    private readonly Mock<IContactService> _mockContactService;
    private readonly ContactDeletedConsumer _consumer;

    public ContactDeletedConsumerTests()
    {
        Mock<ILogger<ContactDeletedConsumer>> mockLogger = new();
        _mockContactService = new Mock<IContactService>();
        _consumer = new ContactDeletedConsumer(mockLogger.Object, _mockContactService.Object);
    }


    [Fact]
    public async void ShouldProcessMessage_ContactDeleted_Success()
    {
        var harness = new InMemoryTestHarness();
        var consumerHarness = harness.Consumer(() => _consumer);
        _mockContactService.Setup(x =>
                x.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await harness.Start();
        try
        {
            var message = new ContactDeleteEvent() { Id = 1 };
            await harness.InputQueueSendEndpoint.Send(message);
            Assert.True(await harness.Sent.Any<ContactDeleteEvent>());
            Assert.True(await consumerHarness.Consumed.Any<ContactDeleteEvent>());
        }
        finally
        {
            await harness.Stop();
        }
    }

    [Fact]
    public async Task ContactDeleted_NotProcessed()
    {
        var harness = new InMemoryTestHarness();
        var consumerHarness = harness.Consumer(() => _consumer);

        await harness.Start();
        try
        {
            var message = new Contact("test", "", "", 0);
            await harness.InputQueueSendEndpoint.Send(message);
            Assert.False(await harness.Sent.Any<ContactDeleteEvent>());
            Assert.False(await consumerHarness.Consumed.Any<ContactDeleteEvent>());
        }
        finally
        {
            await harness.Stop();
        }
    }

    [Fact]
    public async Task ContactDeleted_ThrowsException()
    {
        var harness = new InMemoryTestHarness();
        var consumerHarness = harness.Consumer(() => _consumer);

        _mockContactService.Setup(x =>
            x.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        await harness.Start();
        try
        {
            var message = new ContactDeleteEvent() { Id = 1 };
            await harness.InputQueueSendEndpoint.Send(message);
            Assert.True(await harness.Sent.Any<ContactDeleteEvent>());
            Assert.True(await consumerHarness.Consumed.Any<ContactDeleteEvent>());
        }
        finally
        {
            await harness.Stop();
        }
    }
}