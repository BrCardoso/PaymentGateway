
namespace Application.Shared.Repository
{
    public interface IDataStorageService
    {
        Task<T> Get<T>(Guid id);
        Task<bool> Set<T>(Guid id, T value);
    }
}