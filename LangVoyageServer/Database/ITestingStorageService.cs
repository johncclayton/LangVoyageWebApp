using LangVoyageServer.Models;

namespace LangVoyageServer.Database;

public interface ITestingStorageService : IStorageService
{
    Task<IList<NounProgress>> EnsureAllNounsAreProgressedAsync(int userId, string level);
}