using LangVoyageServer.Configuration;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace LangVoyageServer.Database;

/// <summary>
/// Service interface for data storage operations related to language learning.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Updates or creates language nouns in the database.
    /// </summary>
    /// <param name="data">Array of language nouns to update or create</param>
    /// <param name="saveImmediately">Whether to save changes immediately</param>
    /// <returns>Collection of processed language nouns</returns>
    Task<IEnumerable<LanguageNoun>> UpdateNounsAsync(LanguageNoun[] data, bool saveImmediately = false);
    
    /// <summary>
    /// Creates or updates a user profile.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="req">Update request containing new user data</param>
    /// <returns>Updated user profile or null if invalid</returns>
    Task<UserProfile?> UpsertUserProfileAsync(int id, UpdateUserRequest req);
    
    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="userId">The user ID to search for</param>
    /// <returns>User profile or null if not found</returns>
    Task<UserProfile?> GetUserAsync(int userId);
    
    /// <summary>
    /// Gets nouns for practice using spaced repetition algorithm.
    /// </summary>
    /// <param name="userId">User ID requesting practice nouns</param>
    /// <param name="limit">Maximum number of nouns to return</param>
    /// <returns>List of nouns ready for practice</returns>
    Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit = AppConstants.Database.DefaultNounLimit);
    
    /// <summary>
    /// Gets practice progress for a specific noun and user.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="nounId">Noun ID</param>
    /// <returns>Progress record or null if not found</returns>
    Task<NounProgress?> GetPractiseNounAsync(int userId, int nounId);
    
    /// <summary>
    /// Updates practice progress for a noun.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="nounId">Noun ID</param>
    /// <param name="wasCorrect">Whether the answer was correct</param>
    /// <returns>Updated progress record</returns>
    Task<NounProgress> UpsertNounProgressAsync(int userId, int nounId, bool wasCorrect);
    
    /// <summary>
    /// Deletes practice progress for a specific noun.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="nounId">Noun ID</param>
    /// <returns>Number of deleted records</returns>
    Task<int> DeleteNounProgressAsync(int userId, int nounId);
    
    /// <summary>
    /// Updates all noun progress records for a user (marks all as correct).
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of updated progress records</returns>
    Task<IList<NounProgress>> UpdateAllNounProgressAsync(int userId);
    
    /// <summary>
    /// Deletes all practice progress for a user.
    /// </summary>
    /// <param name="userId">User ID</param>
    Task DeleteAllNounProgressAsync(int userId);
    
    /// <summary>
    /// Gets learning progress summary for a user.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Learning progress response with statistics</returns>
    Task<LearningProgressResponse> GetLearningProgress(int userId);
}