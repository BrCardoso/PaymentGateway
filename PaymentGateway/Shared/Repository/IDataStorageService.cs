
namespace Application.Shared.Repository
{
    public interface IDataStorageService
    {
        T Get<T>(Guid id);
        string Set(Guid id, string value);
    }
}