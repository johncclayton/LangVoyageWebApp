using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace LangVoyageServer.Database;

public interface IStorageService
{
    // for importer.
    Task<IEnumerable<LanguageNoun>> UpdateNounsAsync(LanguageNoun[] data, bool saveImmediately = false);
    
    Task<UserProfile?> UpsertUserProfileAsync(int id, UpdateUserRequest req);
    
    Task<UserProfile?> GetUserAsync(int userId);
    
    Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit = 20);
    
    Task<NounProgress?> GetPractiseNounAsync(int userId, int nounId);
    
    Task<NounProgress> UpsertNounProgressAsync(int userId, int nounId, bool wasCorrect);
    
    Task<int> DeleteNounProgressAsync(int userId, int nounId);
    
    Task<IList<NounProgress>> UpdateAllNounProgressAsync(int userId);
    
    Task DeleteAllNounProgressAsync(int userId);
}