namespace Fiap.TechChallenge.Application.Repositories;

/// <summary>
/// Interface repository to manage contact data from database.
/// </summary>
/// <param name="dbContext">Entity framework database context.</param>
public interface IContactRepository
{
    /// <summary>
    /// Deletes a contact asynchronously from the database.
    /// </summary>
    /// <param name="id">The ID of the contact to be deleted.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a boolean value that specifies whether the contact was successfully deleted.</returns>
    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken);
}