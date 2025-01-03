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

        var existingProfile = await _context.UserProfiles.Where(up => up.Id == id).FirstOrDefaultAsync();
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

    public Task<UserProfile?> GetUserAsync(int userId)
    {
        return _context.UserProfiles.Where(u => u.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<IList<LanguageNoun>> GetNewPractiseNounsAsync(int userId, int limit)
    {
        // use the "noun progress view", to simply fetch a series of nouns that are "next" to be practised.
        return await _context.NounProgressView
            .Where(v => v.UserProfileId == userId)
            .OrderBy(v => v.TimeFrame)
            .Join(_context.Nouns,
                progress => progress.NounId,
                noun => noun.Id,
                (progress, noun) => noun)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<NounProgress> UpsertNounProgressAsync(int userId, int nounId, bool wasCorrect)
    {
        var user = await GetUserAsync(userId);
        if (user == null)
        {
            throw new Exception("No user profile found.");
        }

        var nounProgress = await _context.NounProgresses
            .FirstOrDefaultAsync(np => np.UserProfileId == userId && np.NounId == nounId);

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

    public async Task DeleteAllNounProgressAsync(int userId)
    {
        await _context.NounProgresses
            .Where(np => np.UserProfileId == userId)
            .ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}