using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Application.Services;
using FluentAssertions;
using Moq;

namespace Fiap.TechChallenge.UnitTests.Application;

public class ContactServiceTests
{
    
    [Fact]
    public async Task ShouldDeleteWithSuccess()
    {
        //Arrange
        var contactRepository = new Mock<IContactRepository>();
        contactRepository.Setup(c => c.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var contactService = new ContactService(contactRepository.Object);

        //Act
        var result = await contactService.DeleteAsync(1, CancellationToken.None);

        //Assert
        result.Should().BeTrue();
        contactRepository.Verify(c =>
            c.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task ShouldDeleteWithException()
    {
        //Arrange
        var contactRepository = new Mock<IContactRepository>();
        contactRepository.Setup(c => c.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .Throws<Exception>();

        var contactService = new ContactService(contactRepository.Object);
        
        //Act & Assert
        await Assert.ThrowsAsync<Exception>(() => contactService.DeleteAsync(1, CancellationToken.None));
        contactRepository.Verify(c =>
            c.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}