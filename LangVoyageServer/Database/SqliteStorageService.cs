using LangVoyageServer.Configuration;
using LangVoyageServer.Exceptions;
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

        // Get all existing nouns in one query for better performance
        var nounNames = uniqueSortedByNoun.Select(n => n.Noun).ToList();
        var existingNouns = await _context.Nouns
            .Where(n => nounNames.Contains(n.Noun))
            .ToDictionaryAsync(n => n.Noun, n => n);

        var nounsToAdd = new List<LanguageNoun>();
        var nounsToUpdate = new List<LanguageNoun>();

        foreach (var singleNoun in uniqueSortedByNoun)
        {
            if (existingNouns.TryGetValue(singleNoun.Noun, out var existingNoun))
            {
                existingNoun.Plural = singleNoun.Plural;
                nounsToUpdate.Add(existingNoun);
            }
            else
            {
                nounsToAdd.Add(singleNoun);
            }
        }

        // Batch operations
        if (nounsToAdd.Count > 0)
        {
            _context.Nouns.AddRange(nounsToAdd);
        }
        
        if (nounsToUpdate.Count > 0)
        {
            _context.Nouns.UpdateRange(nounsToUpdate);
        }

        if (saveImmediately)
        {
            await _context.SaveChangesAsync();
        }

        return uniqueSortedByNoun;
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

    public async Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit = AppConstants.Database.DefaultNounLimit)
    {
        var user = await GetUserAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }

        // Ensure limit is within reasonable bounds
        var safeLimit = Math.Min(limit, AppConstants.Database.MaxNounLimit);

        // use the "noun progress view", to simply fetch a series of nouns that are "next" to be practised.
        return await _context.NounProgressView
            .Where(v => v.UserProfileId == userId && v.NounLevel == user.LanguageLevel)
            .OrderBy(v => v.TimeFrame)
            .Join(_context.Nouns,
                progress => progress.NounId,
                noun => noun.Id,
                (progress, noun) => noun)
            .Take(safeLimit)
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
            throw new UserNotFoundException(userId);
        }

        var nounProgress = await _context.NounProgresses.FindAsync([userId, nounId]);
        if (nounProgress == null)
        {
            // create one!
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

        nounProgress.LastPractised = DateTime.UtcNow;
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
        var allNouns = await GetNewPractiseNounsAsync(userId, AppConstants.Database.MaxNounLimit);

        var result = new List<NounProgress>();
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
            throw new UserNotFoundException(userId);
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
            Username = user.Username ?? throw new InvalidOperationException("Username cannot be null"),
            LanguageLevel = user.LanguageLevel ?? throw new InvalidOperationException("Language level cannot be null"),
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