using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Database;

public class SqliteStorageService : IStorageService
{
    internal readonly LangServerDbContext _context;

    public SqliteStorageService(LangServerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LanguageNoun>> UpdateNounsAsync(LanguageNoun[] data, bool saveImmediately = false)
    {
        var uniqueSortedByNoun = data
            .GroupBy(n => n.Noun)
            .Select(g => g.First())
            .OrderBy(n => n.Noun)
            .ToList();

        // for each, lookup - if its there just update otherwise create. 
        foreach (var singleNoun in uniqueSortedByNoun)
        {
            var existingNoun = _context.Nouns.FirstOrDefault(n => n.Noun == singleNoun.Noun);
            if (existingNoun != null)
            {
                existingNoun.Plural = singleNoun.Plural;
                _context.Nouns.Update(existingNoun);
            }
            else
            {
                _context.Nouns.Add(singleNoun);
            }
        }

        await _context.SaveChangesAsync();

        return data.ToList();
    }

    public async Task<UserProfile?> UpsertUserProfileAsync(int id, UpdateUserRequest req)
    {
        if (req.LanguageLevel == null && req.Username == null)
        {
            return null;
        }

        var existingProfile = _context.UserProfiles.SingleOrDefault(uf => uf.Id == id);
        if (existingProfile != null)
        {
            if (req.Username != null)
            {
                existingProfile.Username = req.Username;
            }

            if (req.LanguageLevel != null)
            {
                existingProfile.LanguageLevel = req.LanguageLevel;
            }
        }
        else
        {
            var newProfile = _context.UserProfiles.Add(new UserProfile()
            {
                Username = req.Username,
                LanguageLevel = req.LanguageLevel
            });

            existingProfile = newProfile.Entity;
        }

        await _context.SaveChangesAsync();

        return existingProfile;
    }

    public async Task<UserProfile?> GetUserAsync(int userId)
    {
        return await _context.UserProfiles.FindAsync(userId);
    }

    public async Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit)
    {
        var user = await GetUserAsync(userId);
        if (user == null)
        {
            // Return empty list instead of throwing for missing user
            return new List<LanguageNoun>();
        }

        // Use the noun progress view to implement spaced repetition algorithm:
        // - Filter by user ID and their current language level
        // - Order by TimeFrame (lower = needs more practice, higher = mastered)
        // - Join with nouns table to get complete noun details
        // - Limit results to requested number
        return await _context.NounProgressView
            .Where(v => v.UserProfileId == userId && v.NounLevel == user.LanguageLevel)
            .OrderBy(v => v.TimeFrame)
            .Join(_context.Nouns,
                progress => progress.NounId,
                noun => noun.Id,
                (progress, noun) => noun)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<NounProgress?> GetPractiseNounAsync(int userId, int nounId)
    {
        return await _context.NounProgresses.FindAsync([userId, nounId]);
    }

    public async Task<NounProgress> UpsertNounProgressAsync(int userId, int nounId, bool wasCorrect)
    {
        var user = await GetUserAsync(userId);
        if (user == null)
        {
            throw new Exception("No user profile found.");
        }

        var nounProgress = await _context.NounProgresses.FindAsync([userId, nounId]);
        if (nounProgress == null)
        {
            // Create new progress record for first-time practice
            // Starting TimeFrame of 1 indicates beginner level
            var newProgress = _context.NounProgresses.Add(new NounProgress
            {
                UserProfileId = user.Id,
                NounId = nounId,
                LastPractised = DateTime.UtcNow,
                TimeFrame = 1
            });

            await _context.SaveChangesAsync();

            return newProgress.Entity;
        }

        // Update existing progress record
        nounProgress.LastPractised = DateTime.UtcNow;
        
        // Implement spaced repetition logic:
        // Correct answers increase TimeFrame (longer intervals between practice)
        // Incorrect answers decrease TimeFrame (more frequent practice needed)
        // TimeFrame cannot go below 0 (minimum practice frequency)
        if (wasCorrect)
        {
            nounProgress.TimeFrame++;
        }
        else if (nounProgress.TimeFrame > 0)
        {
            nounProgress.TimeFrame--;
        }

        await _context.SaveChangesAsync();

        return nounProgress;
    }

    public async Task<int> DeleteNounProgressAsync(int userId, int nounId)
    {
        var theProgress = _context.NounProgresses
            .Where(np => np.UserProfileId == userId && np.NounId == nounId);
        _context.NounProgresses.RemoveRange(theProgress.ToArray());
        return await _context.SaveChangesAsync();
    }

    public async Task<IList<NounProgress>> UpdateAllNounProgressAsync(int userId)
    {
        // Get all available nouns for practice (this uses a high limit to get all nouns)
        var allNouns = await GetNewPractiseNounsAsync(userId, 99999);

        var result = new List<NounProgress>();
        
        // Mark all nouns as practiced correctly - useful for testing scenarios
        // or administrative functions to simulate complete learning progress
        foreach (var noun in allNouns)
        {
            result.Add(await UpsertNounProgressAsync(userId, noun.Id, true));
        }

        return result;
    }

    public async Task DeleteAllNounProgressAsync(int userId)
    {
        var toRemove = _context.NounProgresses
            .Where(np => np.UserProfileId == userId);
        _context.NounProgresses.RemoveRange(toRemove.ToArray());
        await _context.SaveChangesAsync();
    }

    public async Task<LearningProgressResponse> GetLearningProgress(int userId)
    {
        var user = await GetUserAsync(userId);
        if (user == null)
        {
            throw new Exception("No user profile found.");
        }

        var nounsAtThisLevelCount = await _context.Nouns
            .CountAsync(n => n.Level == user.LanguageLevel);

        var progress = await _context.NounProgressView
            .Where(p => p.UserProfileId == userId && p.NounLevel == user.LanguageLevel)
            .GroupBy(p => p.TimeFrame)
            .Select(g => new { TimeFrame = g.Key, Count = g.Count() })
            .ToListAsync();

        LearningProgressResponse response = new()
        {
            Username = user.Username ?? throw new InvalidOperationException(),
            LanguageLevel = user.LanguageLevel ?? throw new InvalidOperationException(),
            TotalNouns = nounsAtThisLevelCount,
            NounProgresses = new int[progress.Max(p => p.TimeFrame) + 1]
        };
        
        foreach (var item in progress)
        {
            response.NounProgresses[item.TimeFrame] = item.Count;
        }
        
        return response;
    }
}