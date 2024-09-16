namespace Fiap.TechChallenge.Application.Services.Interfaces;

public interface IContactService
{
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken);
}