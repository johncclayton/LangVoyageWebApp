using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace LangVoyageServer.Database;

/// <summary>
/// Provides data storage and retrieval services for the Language Voyage application.
/// This service handles user profiles, language nouns, and learning progress tracking.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Updates or inserts multiple language nouns in the database.
    /// This method is primarily used by the data importer to batch update noun collections.
    /// </summary>
    /// <param name="data">Array of language nouns to update or insert</param>
    /// <param name="saveImmediately">If true, saves changes to database immediately; otherwise defers save operation</param>
    /// <returns>A task representing the asynchronous operation, containing the collection of updated language nouns</returns>
    Task<IEnumerable<LanguageNoun>> UpdateNounsAsync(LanguageNoun[] data, bool saveImmediately = false);
    
    /// <summary>
    /// Creates or updates a user profile with the specified information.
    /// If the user exists, updates the provided fields; otherwise creates a new user profile.
    /// </summary>
    /// <param name="id">The unique identifier of the user profile</param>
    /// <param name="req">The update request containing the new user information (username and/or language level)</param>
    /// <returns>A task representing the asynchronous operation, containing the updated user profile or null if no changes were made</returns>
    Task<UserProfile?> UpsertUserProfileAsync(int id, UpdateUserRequest req);
    
    /// <summary>
    /// Retrieves a user profile by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <returns>A task representing the asynchronous operation, containing the user profile or null if not found</returns>
    Task<UserProfile?> GetUserAsync(int userId);
    
    /// <summary>
    /// Retrieves a list of language nouns for practice using spaced repetition algorithm.
    /// Returns nouns ordered by their learning progress, prioritizing those that need more practice.
    /// Only returns nouns matching the user's current language level.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="limit">Maximum number of nouns to return (default: 20)</param>
    /// <returns>A task representing the asynchronous operation, containing a list of language nouns ready for practice</returns>
    /// <exception cref="Exception">Thrown when no user profile is found for the specified userId</exception>
    Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit = 20);
    
    /// <summary>
    /// Retrieves the learning progress for a specific noun and user combination.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="nounId">The unique identifier of the noun</param>
    /// <returns>A task representing the asynchronous operation, containing the noun progress or null if not found</returns>
    Task<NounProgress?> GetPractiseNounAsync(int userId, int nounId);
    
    /// <summary>
    /// Creates or updates the learning progress for a specific noun and user.
    /// Implements spaced repetition logic by adjusting the time frame based on correctness:
    /// - Correct answers increase the time frame (longer intervals between practice)
    /// - Incorrect answers decrease the time frame (more frequent practice needed)
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="nounId">The unique identifier of the noun</param>
    /// <param name="wasCorrect">True if the user answered correctly, false otherwise</param>
    /// <returns>A task representing the asynchronous operation, containing the updated noun progress</returns>
    /// <exception cref="Exception">Thrown when no user profile is found for the specified userId</exception>
    Task<NounProgress> UpsertNounProgressAsync(int userId, int nounId, bool wasCorrect);
    
    /// <summary>
    /// Deletes the learning progress record for a specific noun and user combination.
    /// This operation resets the user's progress for the specified noun.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="nounId">The unique identifier of the noun</param>
    /// <returns>A task representing the asynchronous operation, containing the number of records affected</returns>
    Task<int> DeleteNounProgressAsync(int userId, int nounId);
    
    /// <summary>
    /// Creates or updates progress records for all available nouns for the specified user.
    /// This method marks all nouns as "practiced correctly" and is typically used for testing
    /// or administrative purposes to simulate complete learning progress.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <returns>A task representing the asynchronous operation, containing a list of all updated noun progress records</returns>
    Task<IList<NounProgress>> UpdateAllNounProgressAsync(int userId);
    
    /// <summary>
    /// Deletes all learning progress records for the specified user.
    /// This operation completely resets the user's learning progress and cannot be undone.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task DeleteAllNounProgressAsync(int userId);
    
    /// <summary>
    /// Retrieves comprehensive learning progress statistics for the specified user.
    /// Provides a breakdown of noun progress across different time frames/difficulty levels,
    /// useful for displaying progress charts and learning analytics.
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <returns>A task representing the asynchronous operation, containing the learning progress response with statistics</returns>
    Task<LearningProgressResponse> GetLearningProgress(int userId);
}