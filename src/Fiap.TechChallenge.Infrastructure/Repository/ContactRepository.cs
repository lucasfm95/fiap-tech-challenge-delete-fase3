using Fiap.TechChallenge.Application.Repositories;
using Fiap.TechChallenge.Infrastructure.Context;

namespace Fiap.TechChallenge.Infrastructure.Repository;

public class ContactRepository : IContactRepository
{
    private readonly ContactDbContext _dbContext;

    public ContactRepository(ContactDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    ///  Method to remove a contact 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return false;
        }
        _dbContext.Contacts.Remove(contact);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}