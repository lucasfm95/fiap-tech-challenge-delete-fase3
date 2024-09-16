using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Application.Services.Interfaces;

namespace Fiap.TechChallenge.Application.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        return await contactRepository.DeleteAsync(id, cancellationToken);
    }
}