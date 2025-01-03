using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace LangVoyageServer.Database;

public interface IStorageService
{
    // Task<IEnumerable<LanguageNoun>> GetNounsAsync();
    
    // for importer.
    Task<IEnumerable<LanguageNoun>> UpdateNounsAsync(LanguageNoun[] data, bool saveImmediately = false);
    
    Task<UserProfile?> UpsertUserProfileAsync(int id, UpdateUserRequest req);
    
    Task<UserProfile?> GetUserAsync(int userId);
    
    Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit = 20);
    // Task<NounProgress> CreateNounProgressAsync(int userId, int nounId);
    Task<NounProgress> UpsertNounProgressAsync(int userId, int nounId, bool wasCorrect);
    Task DeleteAllNounProgressAsync(int userId);
}